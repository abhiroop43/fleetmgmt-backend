set NOCOUNT on

-- DROP DATABASE FleetMgmt

CREATE DATABASE FleetMgmt;
GO

USE FleetMgmt;
GO


-- aspnet membership scripts

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory]
    (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [AspNetRoles]
(
    [Id] nvarchar(450) NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetUsers]
(
    [Id] nvarchar(450) NOT NULL,
    [AccessFailedCount] int NOT NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [Email] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [LockoutEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [UserName] nvarchar(256) NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [AspNetRoleClaims]
(
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserClaims]
(
    [Id] int NOT NULL IDENTITY,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserLogins]
(
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserRoles]
(
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [AspNetUserTokens]
(
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

GO

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

GO

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

GO

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

GO

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

GO

INSERT INTO [__EFMigrationsHistory]
    ([MigrationId], [ProductVersion])
VALUES
    (N'20180121061024_CreateIdentitySchema', N'2.2.6-servicing-10079');

GO


--- Identity Server scripts

-- IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
-- BEGIN
--     CREATE TABLE [__EFMigrationsHistory]
--     (
--         [MigrationId] nvarchar(150) NOT NULL,
--         [ProductVersion] nvarchar(32) NOT NULL,
--         CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
--     );
-- END;

-- GO

-- CREATE TABLE [ApiResources]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [Description] nvarchar(1000) NULL,
--     [DisplayName] nvarchar(200) NULL,
--     [Enabled] bit NOT NULL,
--     [Name] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_ApiResources] PRIMARY KEY ([Id])
-- );

-- GO

-- CREATE TABLE [Clients]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [AbsoluteRefreshTokenLifetime] int NOT NULL,
--     [AccessTokenLifetime] int NOT NULL,
--     [AccessTokenType] int NOT NULL,
--     [AllowAccessTokensViaBrowser] bit NOT NULL,
--     [AllowOfflineAccess] bit NOT NULL,
--     [AllowPlainTextPkce] bit NOT NULL,
--     [AllowRememberConsent] bit NOT NULL,
--     [AlwaysIncludeUserClaimsInIdToken] bit NOT NULL,
--     [AlwaysSendClientClaims] bit NOT NULL,
--     [AuthorizationCodeLifetime] int NOT NULL,
--     [BackChannelLogoutSessionRequired] bit NOT NULL,
--     [BackChannelLogoutUri] nvarchar(2000) NULL,
--     [ClientClaimsPrefix] nvarchar(200) NULL,
--     [ClientId] nvarchar(200) NOT NULL,
--     [ClientName] nvarchar(200) NULL,
--     [ClientUri] nvarchar(2000) NULL,
--     [ConsentLifetime] int NULL,
--     [Description] nvarchar(1000) NULL,
--     [EnableLocalLogin] bit NOT NULL,
--     [Enabled] bit NOT NULL,
--     [FrontChannelLogoutSessionRequired] bit NOT NULL,
--     [FrontChannelLogoutUri] nvarchar(2000) NULL,
--     [IdentityTokenLifetime] int NOT NULL,
--     [IncludeJwtId] bit NOT NULL,
--     [LogoUri] nvarchar(2000) NULL,
--     [PairWiseSubjectSalt] nvarchar(200) NULL,
--     [ProtocolType] nvarchar(200) NOT NULL,
--     [RefreshTokenExpiration] int NOT NULL,
--     [RefreshTokenUsage] int NOT NULL,
--     [RequireClientSecret] bit NOT NULL,
--     [RequireConsent] bit NOT NULL,
--     [RequirePkce] bit NOT NULL,
--     [SlidingRefreshTokenLifetime] int NOT NULL,
--     [UpdateAccessTokenClaimsOnRefresh] bit NOT NULL,
--     CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
-- );

-- GO

-- CREATE TABLE [IdentityResources]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [Description] nvarchar(1000) NULL,
--     [DisplayName] nvarchar(200) NULL,
--     [Emphasize] bit NOT NULL,
--     [Enabled] bit NOT NULL,
--     [Name] nvarchar(200) NOT NULL,
--     [Required] bit NOT NULL,
--     [ShowInDiscoveryDocument] bit NOT NULL,
--     CONSTRAINT [PK_IdentityResources] PRIMARY KEY ([Id])
-- );

-- GO

-- CREATE TABLE [ApiClaims]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ApiResourceId] int NOT NULL,
--     [Type] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_ApiClaims] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ApiClaims_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ApiScopes]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ApiResourceId] int NOT NULL,
--     [Description] nvarchar(1000) NULL,
--     [DisplayName] nvarchar(200) NULL,
--     [Emphasize] bit NOT NULL,
--     [Name] nvarchar(200) NOT NULL,
--     [Required] bit NOT NULL,
--     [ShowInDiscoveryDocument] bit NOT NULL,
--     CONSTRAINT [PK_ApiScopes] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ApiScopes_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ApiSecrets]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ApiResourceId] int NOT NULL,
--     [Description] nvarchar(1000) NULL,
--     [Expiration] datetime2 NULL,
--     [Type] nvarchar(250) NULL,
--     [Value] nvarchar(2000) NULL,
--     CONSTRAINT [PK_ApiSecrets] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ApiSecrets_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientClaims]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Type] nvarchar(250) NOT NULL,
--     [Value] nvarchar(250) NOT NULL,
--     CONSTRAINT [PK_ClientClaims] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientClaims_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientCorsOrigins]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Origin] nvarchar(150) NOT NULL,
--     CONSTRAINT [PK_ClientCorsOrigins] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientCorsOrigins_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientGrantTypes]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [GrantType] nvarchar(250) NOT NULL,
--     CONSTRAINT [PK_ClientGrantTypes] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientGrantTypes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientIdPRestrictions]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Provider] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_ClientIdPRestrictions] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientIdPRestrictions_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientPostLogoutRedirectUris]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [PostLogoutRedirectUri] nvarchar(2000) NOT NULL,
--     CONSTRAINT [PK_ClientPostLogoutRedirectUris] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientPostLogoutRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientProperties]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Key] nvarchar(250) NOT NULL,
--     [Value] nvarchar(2000) NOT NULL,
--     CONSTRAINT [PK_ClientProperties] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientProperties_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientRedirectUris]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [RedirectUri] nvarchar(2000) NOT NULL,
--     CONSTRAINT [PK_ClientRedirectUris] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientRedirectUris_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientScopes]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Scope] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_ClientScopes] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientScopes_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ClientSecrets]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ClientId] int NOT NULL,
--     [Description] nvarchar(2000) NULL,
--     [Expiration] datetime2 NULL,
--     [Type] nvarchar(250) NULL,
--     [Value] nvarchar(2000) NOT NULL,
--     CONSTRAINT [PK_ClientSecrets] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ClientSecrets_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [IdentityClaims]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [IdentityResourceId] int NOT NULL,
--     [Type] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_IdentityClaims] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_IdentityClaims_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [IdentityResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [ApiScopeClaims]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [ApiScopeId] int NOT NULL,
--     [Type] nvarchar(200) NOT NULL,
--     CONSTRAINT [PK_ApiScopeClaims] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ApiScopeClaims_ApiScopes_ApiScopeId] FOREIGN KEY ([ApiScopeId]) REFERENCES [ApiScopes] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE INDEX [IX_ApiClaims_ApiResourceId] ON [ApiClaims] ([ApiResourceId]);

-- GO

-- CREATE UNIQUE INDEX [IX_ApiResources_Name] ON [ApiResources] ([Name]);

-- GO

-- CREATE INDEX [IX_ApiScopeClaims_ApiScopeId] ON [ApiScopeClaims] ([ApiScopeId]);

-- GO

-- CREATE INDEX [IX_ApiScopes_ApiResourceId] ON [ApiScopes] ([ApiResourceId]);

-- GO

-- CREATE UNIQUE INDEX [IX_ApiScopes_Name] ON [ApiScopes] ([Name]);

-- GO

-- CREATE INDEX [IX_ApiSecrets_ApiResourceId] ON [ApiSecrets] ([ApiResourceId]);

-- GO

-- CREATE INDEX [IX_ClientClaims_ClientId] ON [ClientClaims] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientCorsOrigins_ClientId] ON [ClientCorsOrigins] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientGrantTypes_ClientId] ON [ClientGrantTypes] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientIdPRestrictions_ClientId] ON [ClientIdPRestrictions] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientPostLogoutRedirectUris_ClientId] ON [ClientPostLogoutRedirectUris] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientProperties_ClientId] ON [ClientProperties] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientRedirectUris_ClientId] ON [ClientRedirectUris] ([ClientId]);

-- GO

-- CREATE UNIQUE INDEX [IX_Clients_ClientId] ON [Clients] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientScopes_ClientId] ON [ClientScopes] ([ClientId]);

-- GO

-- CREATE INDEX [IX_ClientSecrets_ClientId] ON [ClientSecrets] ([ClientId]);

-- GO

-- CREATE INDEX [IX_IdentityClaims_IdentityResourceId] ON [IdentityClaims] ([IdentityResourceId]);

-- GO

-- CREATE UNIQUE INDEX [IX_IdentityResources_Name] ON [IdentityResources] ([Name]);

-- GO

-- INSERT INTO [__EFMigrationsHistory]
--     ([MigrationId], [ProductVersion])
-- VALUES
--     (N'20180118095546_InitialIdentityServerConfigurationDbMigration', N'2.2.6-servicing-10079');

-- GO

-- ALTER TABLE [IdentityResources] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

-- GO

-- ALTER TABLE [IdentityResources] ADD [NonEditable] bit NOT NULL DEFAULT 0;

-- GO

-- ALTER TABLE [IdentityResources] ADD [Updated] datetime2 NULL;

-- GO

-- DECLARE @var0 sysname;
-- SELECT @var0 = [d].[name]
-- FROM [sys].[default_constraints] [d]
--     INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
-- WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ClientSecrets]') AND [c].[name] = N'Value');
-- IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ClientSecrets] DROP CONSTRAINT [' + @var0 + '];');
-- ALTER TABLE [ClientSecrets] ALTER COLUMN [Value] nvarchar(4000) NOT NULL;

-- GO

-- DECLARE @var1 sysname;
-- SELECT @var1 = [d].[name]
-- FROM [sys].[default_constraints] [d]
--     INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
-- WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ClientSecrets]') AND [c].[name] = N'Type');
-- IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [ClientSecrets] DROP CONSTRAINT [' + @var1 + '];');
-- ALTER TABLE [ClientSecrets] ALTER COLUMN [Type] nvarchar(250) NOT NULL;

-- GO

-- ALTER TABLE [ClientSecrets] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

-- GO

-- ALTER TABLE [Clients] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

-- GO

-- ALTER TABLE [Clients] ADD [DeviceCodeLifetime] int NOT NULL DEFAULT 0;

-- GO

-- ALTER TABLE [Clients] ADD [LastAccessed] datetime2 NULL;

-- GO

-- ALTER TABLE [Clients] ADD [NonEditable] bit NOT NULL DEFAULT 0;

-- GO

-- ALTER TABLE [Clients] ADD [Updated] datetime2 NULL;

-- GO

-- ALTER TABLE [Clients] ADD [UserCodeType] nvarchar(100) NULL;

-- GO

-- ALTER TABLE [Clients] ADD [UserSsoLifetime] int NULL;

-- GO

-- DECLARE @var2 sysname;
-- SELECT @var2 = [d].[name]
-- FROM [sys].[default_constraints] [d]
--     INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
-- WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ApiSecrets]') AND [c].[name] = N'Value');
-- IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [ApiSecrets] DROP CONSTRAINT [' + @var2 + '];');
-- ALTER TABLE [ApiSecrets] ALTER COLUMN [Value] nvarchar(4000) NOT NULL;

-- GO

-- DECLARE @var3 sysname;
-- SELECT @var3 = [d].[name]
-- FROM [sys].[default_constraints] [d]
--     INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
-- WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ApiSecrets]') AND [c].[name] = N'Type');
-- IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [ApiSecrets] DROP CONSTRAINT [' + @var3 + '];');
-- ALTER TABLE [ApiSecrets] ALTER COLUMN [Type] nvarchar(250) NOT NULL;

-- GO

-- ALTER TABLE [ApiSecrets] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

-- GO

-- ALTER TABLE [ApiResources] ADD [Created] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

-- GO

-- ALTER TABLE [ApiResources] ADD [LastAccessed] datetime2 NULL;

-- GO

-- ALTER TABLE [ApiResources] ADD [NonEditable] bit NOT NULL DEFAULT 0;

-- GO

-- ALTER TABLE [ApiResources] ADD [Updated] datetime2 NULL;

-- GO

-- CREATE TABLE [ApiProperties]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [Key] nvarchar(250) NOT NULL,
--     [Value] nvarchar(2000) NOT NULL,
--     [ApiResourceId] int NOT NULL,
--     CONSTRAINT [PK_ApiProperties] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_ApiProperties_ApiResources_ApiResourceId] FOREIGN KEY ([ApiResourceId]) REFERENCES [ApiResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE TABLE [IdentityProperties]
-- (
--     [Id] int NOT NULL IDENTITY,
--     [Key] nvarchar(250) NOT NULL,
--     [Value] nvarchar(2000) NOT NULL,
--     [IdentityResourceId] int NOT NULL,
--     CONSTRAINT [PK_IdentityProperties] PRIMARY KEY ([Id]),
--     CONSTRAINT [FK_IdentityProperties_IdentityResources_IdentityResourceId] FOREIGN KEY ([IdentityResourceId]) REFERENCES [IdentityResources] ([Id]) ON DELETE CASCADE
-- );

-- GO

-- CREATE INDEX [IX_ApiProperties_ApiResourceId] ON [ApiProperties] ([ApiResourceId]);

-- GO

-- CREATE INDEX [IX_IdentityProperties_IdentityResourceId] ON [IdentityProperties] ([IdentityResourceId]);

-- GO

-- INSERT INTO [__EFMigrationsHistory]
--     ([MigrationId], [ProductVersion])
-- VALUES
--     (N'20190711061717_FrameworkUpdate', N'2.2.6-servicing-10079');

-- GO

