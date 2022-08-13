using Mic.Core.DataTypes;
using Mic.UserDb.Dal;
using Mic.UserDb.Entities;
using System;
using System.Collections.Generic;

namespace DbData.Service.UnitTest.Base
{
    public class InitUserData
    {
        protected readonly UserDbContext context;

        public readonly Guid GroupAdminId = Guid.NewGuid();
        public readonly Guid GroupUserId = Guid.NewGuid();
        public readonly Guid GroupMemberId = Guid.NewGuid();

        public readonly Guid RoleAdminId = Guid.NewGuid();
        public readonly Guid RoleUserId = Guid.NewGuid();
        public readonly Guid RoleMemberId = Guid.NewGuid();

        public readonly Guid UserId = Guid.NewGuid();
        public readonly Guid UserAdminId = Guid.NewGuid();
        public string UserAdminName;
        public readonly Guid UserMemberId = Guid.NewGuid();

        public readonly string LanguageCode;
        public readonly string LanguageKey;
        public readonly Guid WebPageId_Index = Guid.NewGuid();
        public readonly Guid WebPageId_AddEdit = Guid.NewGuid();

        public readonly Guid WebControllerId = Guid.NewGuid();
        public InitUserData(UserDbContext userContext)
        {
            context = userContext;
            LanguageCode = dStr.RandomText(4).ToLower();
            LanguageKey = dStr.RandomText(8).ToLower();
        }

        public void InitLanguage()
        {
            context.Languages.Add(new ELanguage
            {
                LangCode = LanguageCode,
                Name = "English",
                Native = "en",
                Ordinal = 1,
                IsDefault = true,
                IsRtl = false,
                IsActive = true
            });

            context.LanguageDatas.Add(new ELanguageData
            {
                LangKey = LanguageKey,
                LangCode = LanguageCode,
                IsGroup = false,
                Value = "Key One"
            });

            context.LanguageDataLocals.Add(new ELanguageDataLocal
            {
                LangKey = LanguageKey,
                LangCode = LanguageCode,
                Value = "Key Local"
            });
            context.SaveChanges();
        }

        public void InitUserDb()
        {
            UserAdminName = "admin_" + dStr.RandomText(5);
            // Usergroup
            context.Usergroups.AddRange(new List<EUsergroup>
            {
                new EUsergroup
                {
                    Id = GroupAdminId,
                    Name = "Administrators",
                    Priority = 9999,
                    IsDefault = false,
                    IsRoot = true,
                    IsActive = true
                }
                , new EUsergroup
                {
                    Id = GroupUserId,
                    Name = "Users",
                    Description = "Default group for Backend new User",
                    Priority = 99,
                    IsDefault = false,
                    IsRoot = true,
                    IsActive = true
                }
                , new EUsergroup
                {
                    Id = GroupMemberId,
                    Name = "Members",
                    Description = "Default group for Fontend new Registers",
                    Priority = 9,
                    IsDefault = true,
                    IsRoot = true,
                    IsActive = true
                }
            });
            context.SaveChanges();

            // Role
            var roleAdmin = new ERole
            {
                Id = RoleAdminId,
                Name = "Admin",
                Priority = 9999,
                IsDefault = false,
                IsRoot = true
            };
            var roleUser = new ERole
            {
                Id = RoleUserId,
                Name = "User",
                Description = "Default role for Backend users",
                Priority = 99,
                IsDefault = false,
                IsRoot = true
            };
            var roleMember = new ERole
            {
                Id = RoleMemberId,
                Name = "Member",
                Description = "Default role for Frontend members",
                Priority = 9,
                IsDefault = true,
                IsRoot = true
            };
            context.Roles.AddRange(new List<ERole>
            {
                roleAdmin,
                roleUser,
                roleMember
            });
            context.SaveChanges();

            // User
            var currentTime = DateTime.UtcNow;
            var userAdmin = new EUser
            {
                Id = UserAdminId,
                UsergroupId = GroupAdminId,
                UserName = UserAdminName,
                Password = "123@123Aa",

                Email = "root@localhost",
                EmailVerified = true,
                PhoneNumber = "+11 111 111 1111",
                PhoneNumberVerified = true,

                FullName = "Administrator",
                GivenName = "Admin",
                FamilyName = "adm",

                LangCode = "en",
                Timeout = 0,
                Timezone = 7,

                MustChangePassword = false,
                CreatedDate = currentTime,
                IsActive = true,
                Deleted = false
            };
            userAdmin.HashPassword();
            context.Users.Add(userAdmin);

            var user1 = new EUser
            {
                Id = UserId,
                UsergroupId = GroupUserId,
                UserName = "user1",
                Password = "123@123Aa",

                Email = "user1@localhost",
                EmailVerified = true,
                PhoneNumber = "+11 111 111 1111",
                PhoneNumberVerified = true,

                FullName = "First User",
                GivenName = "User",
                FamilyName = "usr",

                LangCode = "vn",
                Timeout = 10,
                Timezone = 8,

                MustChangePassword = false,
                CreatedDate = currentTime,
                IsActive = true,
                Deleted = false
            };
            user1.HashPassword();
            context.Users.Add(user1);

            var member = new EUser
            {
                Id = UserMemberId,
                UsergroupId = GroupMemberId,
                UserName = "member1",
                Password = "123@123A",

                Email = "member1@localhost",
                EmailVerified = true,
                PhoneNumber = "+11 111 111 1112",
                PhoneNumberVerified = true,

                FullName = "First Member",
                GivenName = "Member",
                FamilyName = "mem",

                LangCode = "vn",
                Timeout = 10,
                Timezone = 8,

                MustChangePassword = false,
                CreatedDate = currentTime,
                IsActive = true,
                Deleted = false
            };
            member.HashPassword();
            context.Users.Add(member);
            context.SaveChanges();

            userAdmin.Roles = new List<ERole> { roleAdmin, roleUser, roleMember };
            user1.Roles = new List<ERole> { roleUser, roleMember };
            member.Roles = new List<ERole> { roleMember };
            context.SaveChanges();
        }

        public void InitWebPage()
        {
            context.WebPages.Add(new EWebPage
            {
                Action = "Index",
                AllowAnonymous = true,
                Id = WebPageId_Index,
                CreateDate = DateTime.Now,
                Description = "",
                DisplayName = "Index",
                ModifyDate = DateTime.Now,
                Permissions = 1,
                WebControllerId = WebControllerId                
            });

            context.WebPages.Add(new EWebPage
            {
                Action = "AddEdit",
                AllowAnonymous = true,
                Id = WebPageId_AddEdit,
                CreateDate = DateTime.Now,
                Description = "",
                DisplayName = "AddEdit",
                ModifyDate = DateTime.Now,
                Permissions = 1,
                WebControllerId = WebControllerId
            });

            context.SaveChanges();
        }
    }
}
