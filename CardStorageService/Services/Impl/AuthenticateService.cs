﻿using CardStorageService.Data;
using CardStorageService.Models;
using CardStorageService.Models.Requests;
using CardStorageService.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardStorageService.Services.Impl
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IServiceScopeFactory scopeFactory;

        private readonly Dictionary<string, SessionInfo> sessions = new();

        public const string SecretKey = "12313265165165";

        public AuthenticateService(IServiceScopeFactory ScopeFactory)
        {
            scopeFactory = ScopeFactory;
        }

        public AuthenticationResponse Login(AuthenticationRequest authenticationRequest)
        {
            using IServiceScope scope = scopeFactory.CreateScope();
            CardStorageServiceDbContext context = scope.ServiceProvider.GetService<CardStorageServiceDbContext>();

            Account account = !string.IsNullOrWhiteSpace(authenticationRequest.Login) ? FindAccountByLogin(context, authenticationRequest.Login) : null;

            if (account == null)
            {
                return new AuthenticationResponse { Status = AuthenticationStatus.UserNotFound };
            }

            if (!PasswordUtils.VerifyPassword(authenticationRequest.Password, account.PasswordSalt, account.PasswordHash))
            {
                return new AuthenticationResponse { Status = AuthenticationStatus.InvalidPassword };
            }

            AccountSession session = new AccountSession
            {
                AccountId = account.AccountId,
                SessionToken = CreateSessionToken(account),
                TimeCreated = DateTime.Now,
                TimeLastRequest = DateTime.Now,
                IsClosed = false,
            };

            context.AccountSessions.Add(session);
            context.SaveChanges();

            SessionInfo sessionInfo = GetSessionInfo(account, session);

            lock (sessions)
            {
                sessions[sessionInfo.SessionToken] = sessionInfo;
            }
            
            return new AuthenticationResponse
            {
                Status = AuthenticationStatus.Success,
                SessionInfo = sessionInfo
            };
        }

        public SessionInfo GetSessionInfo(string sessionToken)
        {
            SessionInfo sessionInfo;

            lock (sessions)
            {
                sessions.TryGetValue(sessionToken, out sessionInfo);
            }

            if (sessionInfo == null)
            {
                using IServiceScope scope = scopeFactory.CreateScope();
                CardStorageServiceDbContext context = scope.ServiceProvider.GetRequiredService<CardStorageServiceDbContext>();

                AccountSession session = context.AccountSessions.FirstOrDefault(s => s.SessionToken == sessionToken);

                if (session == null)
                {
                    return null;
                }

                Account account = context.Accounts.FirstOrDefault(acc => acc.AccountId == session.AccountId);
                sessionInfo = GetSessionInfo(account, session);

                if (sessionInfo != null)
                {
                    lock (sessions)
                    {
                        sessions[sessionToken] = sessionInfo;
                    }
                }

            }

            return sessionInfo;

        }


        private SessionInfo GetSessionInfo(Account account, AccountSession accountSession)
        {
            return new SessionInfo
            {
                SessionToken = accountSession.SessionToken,
                SessionId = accountSession.SessionId,
                Account = new AccountDto
                {
                    AccountId = account.AccountId,
                    EMail = account.EMail,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    SecondName = account.SecondName,
                    Locked = account.Locked,
                }
            };
        }

        private string CreateSessionToken(Account account)
        {
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                            new Claim(ClaimTypes.Email, account.EMail),
                        }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);



        }

        private Account? FindAccountByLogin(CardStorageServiceDbContext context, string login)
        {
            return context.Accounts.FirstOrDefault(acc => acc.EMail == login);
        }


    }
}