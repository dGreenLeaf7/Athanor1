-- Copyright (c) 2026 CODE Éxitos. All rights reserved.
-- Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
USE [master]
GO
CREATE DATABASE [FLT]
GO
ALTER DATABASE [FLT] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FLT].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [FLT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FLT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FLT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FLT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FLT] SET ARITHABORT OFF 
GO
ALTER DATABASE [FLT] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FLT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FLT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FLT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FLT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FLT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FLT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FLT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FLT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FLT] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FLT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FLT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FLT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FLT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FLT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FLT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FLT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FLT] SET RECOVERY FULL 
GO
ALTER DATABASE [FLT] SET  MULTI_USER 
GO
ALTER DATABASE [FLT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FLT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FLT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [FLT] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FLT] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'FLT', N'ON'
GO
USE [FLT]
GO
CREATE ROLE [db_Flight_Creator]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppSetting](
	[AppId] [int] IDENTITY(1,1) NOT NULL,
	[TimeZoneCode] [varchar](10) NOT NULL,
	[SMTPServerAndPort] [varchar](255) NULL,
	[SMTPAuthentication] [varchar](255) NULL,
	[SMTPEnableSSL] [bit] NULL,
	[SMTPUserName] [varchar](255) NULL,
	[SMTPPassword] [varchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AppSetting_1] PRIMARY KEY CLUSTERED 
(
	[AppId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APUType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_APUType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuditLog](
	[auditID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[auditDateTime] [datetime] NOT NULL,
	[auditAction] [char](1) NOT NULL,
	[auditStampUserName] [nvarchar](20) NULL,
	[tableName] [varchar](128) NULL,
	[PK] [varchar](128) NULL,
	[fieldName] [varchar](128) NULL,
	[oldValue] [nvarchar](max) NULL,
	[newValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AuditLog] PRIMARY KEY CLUSTERED 
(
	[auditID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FltModule](
	[ModuleName] [nvarchar](50) NOT NULL,
	[ModuleDescription] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FltModule_1] PRIMARY KEY CLUSTERED 
(
	[ModuleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FltRole](
	[ProgId] [int] NOT NULL,
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleDescription] [nvarchar](255) NULL,
	[IsProgramManager] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FltRole_1] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FltRoleModulePermissions](
	[RoleId] [int] NOT NULL,
	[ModuleName] [nvarchar](50) NOT NULL,
	[CanView] [bit] NULL,
	[CanEdit] [bit] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FltRoleModulePermissions_1] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[ModuleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FltUser](
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordSalt] [nvarchar](128) NULL,
	[Email] [nvarchar](255) NOT NULL,
	[PasswordQuestion] [nvarchar](255) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsSuperUser] [bit] NULL,
	[IsApproved] [bit] NULL,
	[IsLockedOut] [bit] NULL,
	[utcLastLoginDate] [datetime] NULL,
	[utcLastLockoutDate] [datetime] NULL,
	[utcLastPasswordChangedDate] [datetime] NULL,
	[FailedPasswordAttemptCount] [int] NULL,
	[utcFailedPasswordAttemptWindowStart] [datetime] NULL,
	[FailedPasswordAnswerAttemptCount] [int] NULL,
	[utcFailedPasswordAnswerAttemptWindowStart] [datetime] NULL,
	[DisplayName] [nvarchar](30) NULL,
	[Location] [nvarchar](30) NULL,
	[CountryCode] [nvarchar](20) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[MgrUserName] [nvarchar](20) NULL,
	[Department] [nvarchar](30) NULL,
	[Title] [nvarchar](30) NULL,
	[Company] [nvarchar](30) NULL,
	[DefaultProgId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FltUser_1] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FltUserInRole](
	[UserName] [nvarchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_FltUserInRole_1] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentId] [int] NOT NULL,
	[DocumentContent] [varbinary](max) NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentInfo](
	[DocumentId] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[DocumentTypeId] [int] NOT NULL,
	[DocumentName] [varchar](255) NOT NULL,
	[DocumentDescription] [nvarchar](255) NULL,
	[DocumentSize] [int] NULL,
	[DocumentGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Document_1] PRIMARY KEY CLUSTERED 
(
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DocumentType](
	[DocumentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[FileExtension] [varchar](10) NOT NULL,
	[MimeType] [varchar](255) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_DocType_1] PRIMARY KEY CLUSTERED 
(
	[DocumentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pilot](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Pilot_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emission](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Emission_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Engineer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Engineer_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EngineeringAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NULL,
 CONSTRAINT [PK_EngineeringAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EngineType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_EngineType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IssueCode](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_IssueCode_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LengthUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_LengthUnit_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiquidFill](
	[LiquidFillId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[LiquidGroupId] [int] NOT NULL,
	[LiquidTypeId] [int] NOT NULL,
	[LiquidUnitId] [int] NOT NULL,
	[LiquidAmt] [decimal](9, 3) NULL,
	[AirframeHoursReading] [decimal](9, 1) NULL,
	[LiquidFillApprover] [nvarchar](30) NULL,
	[LiquidFillLocation] [nvarchar](255) NOT NULL,
	[LiquidFillGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[HobbsReading] [decimal](9, 1) NOT NULL,
 CONSTRAINT [PK_LiquidFill_1] PRIMARY KEY CLUSTERED 
(
	[LiquidFillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiquidGroup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_LiquidGroup_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiquidType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_LiquidType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LiquidUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_LiquidUnit_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[LocactionId] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[TimeZoneId] [int] NOT NULL,
	[LocationName] [nvarchar](50) NOT NULL,
	[LocationGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Location_1] PRIMARY KEY CLUSTERED 
(
	[LocactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Manufacturer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Manufacturer_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaturityDef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_MaturityDef_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MechAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_MechAction_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Model](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Model_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Program](
	[ProgID] [int] IDENTITY(1,1) NOT NULL,
	[ProgName] [nvarchar](50) NOT NULL,
	[ProgDescription] [nvarchar](255) NULL,
	[ProgVersion] [nvarchar](50) NULL,
	[ProgramLogo] [nvarchar](255) NULL,
	[ReportLogo] [nvarchar](255) NULL,
	[YearLogo] [nvarchar](255) NULL,
	[ProgStatus] [tinyint] NULL,
	[DateOpened] [varchar](50) NULL,
	[DateClosed] [varchar](50) NULL,
	[ActualValue] [money] NULL,
	[ProjectCode] [varchar](50) NULL,
	[ActivityCode] [varchar](50) NULL,
	[DeptCode] [varchar](50) NULL,
	[CompanyName] [nvarchar](255) NULL,
	[NearEndUsage] [int] NULL,
	[AvgDailyUsage] [int] NULL,
	[WeeklyFlightDays] [int] NULL,
	[ProgramGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Program_1] PRIMARY KEY CLUSTERED 
(
	[ProgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Route](
	[RouteId] [int] IDENTITY(1,1) NOT NULL,
	[LocationId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[TimeZoneId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Route_1] PRIMARY KEY CLUSTERED 
(
	[RouteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mechanic](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Mechanic_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[Code] [nvarchar](2) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Region_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Test](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Test_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoadingConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Cargo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Propeller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_Propeller_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropellerType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_PropellerType_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flight](
	[FlightId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[SchedRouteId] [int] NOT NULL,
	[FlownRouteId] [int] NULL,
	[FlightDate] [varchar](10) NOT NULL,
	[FlightDateSort] [varchar](10) NULL,
	[WorkShiftIndicatorId] [int] NOT NULL,
	[LoadingConfigId] [int] NULL,
	[TakeoffWeight] [int] NULL,
	[PilotId] [int] NOT NULL,
	[PreFlightFtiOK] [smallint] NULL,
	[PreFlightTelemetryOK] [smallint] NULL,
	[PreFlightTelemetryCheckComplete] [smallint] NULL,
	[OilLevelIndicatorStart] [smallint] NULL,
	[HydraulicLevelIndicatorStart] [smallint] NULL,
	[UsedMeterOffset] [decimal](9, 1) NULL,
	[MeterUnitId] [int] NULL,
	[DepartureTime] [varchar](8) NOT NULL,
	[DepartureDateTime_Local] [datetime] NULL,
	[DepartureDateTime_UTC] [datetime] NULL,
	[EngineHoursStart] [decimal](7, 1) NULL,
	[HobbsStart] [decimal](9, 1) NOT NULL,
	[ReturnTime] [varchar](8) NOT NULL,
	[ReturnDateTime_Local] [datetime] NULL,
	[ReturnDateTime_UTC] [datetime] NULL,
	[HobbsEnd] [decimal](9, 1) NOT NULL,
	[EngineHoursEnd] [decimal](7, 1) NULL,
	[PostFlightFtiOK] [smallint] NULL,
	[PostFlightTelemetryCheckComplete] [smallint] NULL,
	[NacelleCheckBuildup] [smallint] NULL,
	[NacelleCheckDiscoloration] [smallint] NULL,
	[FlightGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[AirframeWeightID] [int] NOT NULL,
	[Cargo1WeightId] [int] NOT NULL,
	[Cargo2WeightId] [int] NOT NULL,
	[BallastWeightID] [int] NOT NULL,
	[AirframeHoursStart] [decimal](9, 1) NOT NULL,
	[AirframeHoursEnd] [decimal](9, 1) NOT NULL,
	[IsApproved] [bit] NULL,
	[ApprovedBy] [nvarchar](20) NULL,
	[UTCDateApproved] [datetime] NULL,
	[AircraftEngineHoursCurrent] [decimal](11, 1) NULL,
 CONSTRAINT [PK_Flight_1] PRIMARY KEY CLUSTERED 
(
	[FlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightComment](
	[FlightCommentId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[ComponentId] [int] NOT NULL,
	[CommentTime] [varchar](8) NOT NULL,
	[CommentDateTime_UTC] [datetime] NOT NULL,
	[CommentDateTime_Local] [datetime] NOT NULL,
	[PilotComment] [nvarchar](2048) NULL,
	[CommentRating] [smallint] NOT NULL,
	[FlightCommentGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[IssueCodeId] [int] NOT NULL,
 CONSTRAINT [PK_FlightComment_1] PRIMARY KEY CLUSTERED 
(
	[FlightCommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightEngineRun](
	[FlightEngineRunId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[RunStartTime] [varchar](8) NOT NULL,
	[RunEndTime] [varchar](8) NOT NULL,
	[EngineSpeed] [int] NOT NULL,
	[RunStartDateTime_Local] [datetime] NOT NULL,
	[RunStartDateTime_UTC] [datetime] NOT NULL,
	[RunEndDateTime_Local] [datetime] NOT NULL,
	[RunEndDateTime_UTC] [datetime] NOT NULL,
	[FlightEngineRunGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FlightEngineRun] PRIMARY KEY CLUSTERED 
(
	[FlightEngineRunId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FlightRegionTime](
	[FlightRegionTimeId] [int] IDENTITY(1,1) NOT NULL,
	[FlightId] [int] NOT NULL,
	[RegionId] [int] NOT NULL,
	[Hours] [decimal](7, 1) NOT NULL,
	[FlightRegionTimeGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_FlightRegionTime_1] PRIMARY KEY CLUSTERED 
(
	[FlightRegionTimeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UTCTimeZone](
	[TimeZoneId] [int] IDENTITY(1,1) NOT NULL,
	[TimeZoneCode] [varchar](10) NOT NULL,
	[TimeZoneOffset] [float] NOT NULL,
	[TimeZoneDescr] [nvarchar](255) NOT NULL,
	[DaylightSavingTime] [bit] NULL,
	[DaylightSavingOffset] [float] NULL,
	[DaylightSavingBegin] [datetime] NULL,
	[DaylightSavingEnd] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_UTCTimeZone_1] PRIMARY KEY CLUSTERED 
(
	[TimeZoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Aircraft](
	[AircraftId] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[CurrentLocationId] [int] NULL,
	[MSN] [varchar](20) NOT NULL,
	[ShipNbr] [varchar](15) NOT NULL,
	[SerialNbr] [varchar](6) NOT NULL,
	[ModelId] [int] NOT NULL,
	[Description] [nvarchar](255) NOT NULL,
	[EngineerId] [int] NOT NULL,
	[MechanicId] [int] NOT NULL,
	[RegistrationNbr] [varchar](10) NOT NULL,
	[LiveryColor] [varchar](10) NOT NULL,
	[AircraftStatusId] [int] NOT NULL,
	[ManufacturerId] [int] NOT NULL,
	[MeterUnitId] [int] NULL,
	[AirframeHoursStart] [decimal](9, 1) NULL,
	[TotalRequiredUsage] [decimal](7, 1) NULL,
	[EstEndOfTesting] [datetime] NULL,
	[ActEndOfTesting] [datetime] NULL,
	[AircraftGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[AirframeHoursCurrent] [decimal](9, 1) NOT NULL,
	[AirframeHoursOffset] [decimal](9, 1) NOT NULL,
	[AircraftEngineHoursCurrent] [decimal](11, 1) NULL,
 CONSTRAINT [PK_Aircraft_1] PRIMARY KEY CLUSTERED 
(
	[AircraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftComment](
	[AircraftCommentId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[EngineerId] [int] NOT NULL,
	[ComponentId] [int] NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[CommentRating] [smallint] NOT NULL,
	[Comments] [nvarchar](2048) NULL,
	[AircraftCommentGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[IssueCodeId] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftComponent](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AircraftComponent_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftConfiguration](
	[AircraftConfigurationId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[ConfigurationDescription] [nvarchar](255) NOT NULL,
	[ConfigurationStartDate] [datetime] NOT NULL,
	[ConfigurationEndDate] [datetime] NULL,
	[ConfigurationMeterStart] [decimal](9, 1) NULL,
	[ConfigurationMeterEnd] [decimal](9, 1) NULL,
	[ConfigurationMeterOffset] [decimal](9, 1) NULL,
	[MeterUnitId] [int] NULL,
	[ModelId] [int] NULL,
	[EngineMaturityId] [int] NOT NULL,
	[EngineSerialNbr] [nvarchar](20) NOT NULL,
	[EngineTypeId] [int] NOT NULL,
	[EngineEmissionId] [int] NOT NULL,
	[APUTypeId] [int] NOT NULL,
	[APUEmissionId] [int] NOT NULL,
	[APUMaturityId] [int] NOT NULL,
	[APUSerialNbr] [nvarchar](30) NOT NULL,
	[NoseGear] [nvarchar](30) NOT NULL,
	[MainGear] [nvarchar](30) NOT NULL,
	[PropellerTypeId] [int] NOT NULL,
	[PropellerModelNbr] [nvarchar](20) NOT NULL,
	[PropellerSerialNbr] [nvarchar](20) NOT NULL,
	[AvionicsVersion] [nvarchar](20) NOT NULL,
	[EmissionLevel] [nvarchar](20) NOT NULL,
	[AircraftConfigurationGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AircraftConfiguration_1] PRIMARY KEY CLUSTERED 
(
	[AircraftConfigurationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftLocation](
	[AircraftLocationId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[ArrivalDate] [datetime] NOT NULL,
	[RequiredUsage] [decimal](12, 1) NOT NULL,
	[AccumUsage] [decimal](12, 1) NOT NULL,
	[EstEndOfTest] [datetime] NULL,
	[ActEndOfTest] [datetime] NULL,
	[LastLocation] [bit] NULL,
	[AircraftLocationGUID] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AircraftLocation_1] PRIMARY KEY CLUSTERED 
(
	[AircraftLocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftStatus](
	[AircraftStatusId] [int] IDENTITY(1,1) NOT NULL,
	[AircraftId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[EngineerComment] [nvarchar](2048) NULL,
	[HangarComment] [nvarchar](2048) NULL,
	[UTCDateModified] [datetime] NOT NULL,
 CONSTRAINT [PK_AircraftStatus] PRIMARY KEY CLUSTERED 
(
	[AircraftStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftStatusDef](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AircraftStatusDef_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AircraftTestXref](
	[AircraftId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_AircraftTestXref] PRIMARY KEY CLUSTERED 
(
	[AircraftId] ASC,
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeightDef](
	[WeightDefId] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[WeightDefTypeId] [int] NOT NULL,
	[WeightDefDescr] [nvarchar](50) NOT NULL,
	[Weight] [int] NOT NULL,
	[WeightUnitId] [int] NOT NULL,
	[SortOrder] [int] NOT NULL,
	[WeightDefGUID] [uniqueidentifier] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeightDefType](
	[WeightDefTypeId] [int] IDENTITY(1,1) NOT NULL,
	[WeightDefTypeDescr] [nvarchar](50) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeightUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WOEngineerComment](
	[EngineerCommentsId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[ComponentId] [int] NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[CommentRating] [smallint] NOT NULL,
	[Comments] [nvarchar](2048) NULL,
	[EngineerCommentGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[IssueCodeId] [int] NOT NULL,
 CONSTRAINT [PK_WOEngineerComment_1] PRIMARY KEY CLUSTERED 
(
	[EngineerCommentsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WOInstruction](
	[WOInstructionsId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[InstructionLine] [nvarchar](2048) NULL,
	[WOInstructionGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WOInstruction_1] PRIMARY KEY CLUSTERED 
(
	[WOInstructionsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WOPerformedWork](
	[PerformedWorkId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[PerformedWorkLine] [nvarchar](2048) NULL,
	[WOPerformedWorkGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WOPerformedWork_1] PRIMARY KEY CLUSTERED 
(
	[PerformedWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WOReason](
	[WOReasonId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[ReasonLine] [nvarchar](2048) NULL,
	[WOReasonGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WOReason] PRIMARY KEY CLUSTERED 
(
	[WOReasonId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrder](
	[WorkOrderId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderNbr] [nvarchar](20) NOT NULL,
	[ProgId] [int] NOT NULL,
	[AircraftId] [int] NOT NULL,
	[LocationId] [int] NOT NULL,
	[WorkOrderTypeId] [int] NOT NULL,
	[WorkOrderStatusId] [int] NOT NULL,
	[EngineerId] [int] NOT NULL,
	[EngineeringActionId] [int] NOT NULL,
	[MechanicId] [int] NOT NULL,
	[RequestDate] [varchar](10) NOT NULL,
	[FaultOccurredDate] [varchar](10) NOT NULL,
	[FaultOccurredMeter] [decimal](8, 1) NOT NULL,
	[FISComplaintNbr] [nvarchar](8) NOT NULL,
	[SquawkNbr] [nvarchar](20) NOT NULL,
	[DueDate] [varchar](10) NOT NULL,
	[DueTime] [varchar](8) NOT NULL,
	[MeterStartWork] [decimal](8, 1) NOT NULL,
	[FinishDate] [varchar](10) NOT NULL,
	[FinishTime] [varchar](8) NOT NULL,
	[MeterEndWork] [decimal](8, 1) NOT NULL,
	[AircraftDispostion] [nvarchar](50) NOT NULL,
	[AircraftIssueAlert] [bit] NOT NULL,
	[MaintenanceIssueAlert] [bit] NOT NULL,
	[WorkOrderGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkOrder_1] PRIMARY KEY CLUSTERED 
(
	[WorkOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrderDocXref](
	[WorkOrderId] [int] NOT NULL,
	[DocumentId] [int] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NULL,
	[UTCDateModified] [datetime] NULL,
	[StampUserName] [nvarchar](20) NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkOrderDocXref_1] PRIMARY KEY CLUSTERED 
(
	[WorkOrderId] ASC,
	[DocumentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrderPart](
	[WorkOrderPartId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[PartName] [nvarchar](20) NOT NULL,
	[PartDescription] [nvarchar](255) NOT NULL,
	[FisIDRemoved] [nvarchar](20) NOT NULL,
	[PartNbrFailed] [int] NOT NULL,
	[FisIDAdded] [nvarchar](20) NOT NULL,
	[PartModule] [nvarchar](20) NOT NULL,
	[PartNbrRemoved] [int] NOT NULL,
	[PartNbrAdded] [int] NOT NULL,
	[WorkOrderPartGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkOrderPart_1] PRIMARY KEY CLUSTERED 
(
	[WorkOrderPartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrderStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkOrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkOrderType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Progid] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkOrderType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkShift](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProgId] [int] NOT NULL,
	[StringValue] [nvarchar](255) NOT NULL,
	[SortOrder] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
 CONSTRAINT [PK_WorkShift_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WOMechanicComment](
	[WOMechanicCommentsId] [int] IDENTITY(1,1) NOT NULL,
	[WorkOrderId] [int] NOT NULL,
	[ComponentId] [int] NOT NULL,
	[CommentDate] [datetime] NOT NULL,
	[CommentRating] [smallint] NOT NULL,
	[Comments] [nvarchar](2048) NULL,
	[WOMechanicCommentGUID] [uniqueidentifier] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[UTCDateCreated] [datetime] NOT NULL,
	[UTCDateModified] [datetime] NOT NULL,
	[StampUserName] [nvarchar](20) NOT NULL,
	[ConcurrencyId] [timestamp] NOT NULL,
	[IssueCodeId] [int] NOT NULL,
 CONSTRAINT [PK_WOMechanicComment_1] PRIMARY KEY CLUSTERED 
(
	[WOMechanicCommentsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 75, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
CREATE NONCLUSTERED INDEX [IDX_Flight_KeyInfo] ON [dbo].[Flight]
(
	[AircraftId] ASC,
	[LocationId] ASC,
	[FlightDateSort] ASC,
	[WorkShiftIndicatorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180702-093706] ON [dbo].[Flight]
(
	[AircraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180702-081625] ON [dbo].[FlightComment]
(
	[FlightId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180702-081710] ON [dbo].[AircraftComment]
(
	[AircraftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180702-081747] ON [dbo].[WOEngineerComment]
(
	[WorkOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [NonClusteredIndex-20180702-081808] ON [dbo].[WOMechanicComment]
(
	[WorkOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AppSetting] ADD  DEFAULT ((0)) FOR [SMTPEnableSSL]
GO
ALTER TABLE [dbo].[AppSetting] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AppSetting] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AppSetting] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[AppSetting] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[APUType] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[APUType] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AuditLog] ADD  CONSTRAINT [DF_AuditLog_auditID]  DEFAULT (newid()) FOR [auditID]
GO
ALTER TABLE [dbo].[FltModule] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FltModule] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FltModule] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FltModule] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[FltRole] ADD  DEFAULT ((0)) FOR [IsProgramManager]
GO
ALTER TABLE [dbo].[FltRole] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FltRole] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FltRole] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FltRole] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT ((0)) FOR [CanView]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT ((0)) FOR [CanEdit]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [IsSuperUser]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((1)) FOR [IsApproved]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [IsLockedOut]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [FailedPasswordAttemptCount]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [FailedPasswordAnswerAttemptCount]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [DefaultProgId]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FltUser] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[FltUserInRole] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FltUserInRole] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FltUserInRole] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[DocumentInfo] ADD  CONSTRAINT [DF__Document__ProgId__4E53A1AA]  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[DocumentInfo] ADD  CONSTRAINT [DF_Document_DocumentGUID]  DEFAULT (newid()) FOR [DocumentGUID]
GO
ALTER TABLE [dbo].[DocumentInfo] ADD  CONSTRAINT [DF__Document__IsDele__503BEA1C]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DocumentInfo] ADD  CONSTRAINT [DF__Document__UTCDat__51300E55]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[DocumentInfo] ADD  CONSTRAINT [DF__Document__UTCDat__5224328E]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[DocumentType] ADD  CONSTRAINT [DF__DocumentT__MimeTyp__4A8310C6]  DEFAULT ((0)) FOR [MimeType]
GO
ALTER TABLE [dbo].[Pilot] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Pilot] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Pilot] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Emission] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Emission] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Emission] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Engineer] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Engineer] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Engineer] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EngineeringAction] ADD  CONSTRAINT [DF_EngineeringAction_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EngineType] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[EngineType] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[EngineType] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[IssueCode] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[IssueCode] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[IssueCode] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LengthUnit] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[LengthUnit] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[LengthUnit] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  CONSTRAINT [DF_LiquidFill_LiquidFillGUID]  DEFAULT (newid()) FOR [LiquidFillGUID]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  CONSTRAINT [DF__LiquidFil__IsAct__4E88ABD4]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  CONSTRAINT [DF__LiquidFil__IsDel__4F7CD00D]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  CONSTRAINT [DF__LiquidFil__UTCDa__5070F446]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  CONSTRAINT [DF__LiquidFil__UTCDa__5165187F]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[LiquidFill] ADD  DEFAULT ((0)) FOR [HobbsReading]
GO
ALTER TABLE [dbo].[LiquidGroup] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[LiquidGroup] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[LiquidGroup] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LiquidType] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[LiquidType] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[LiquidType] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LiquidUnit] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[LiquidUnit] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[LiquidUnit] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF__Location__ProgId__117F9D94]  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF_Location_LocationGUID]  DEFAULT (newid()) FOR [LocationGUID]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF__Location__IsActi__1273C1CD]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF__Location__IsDele__1367E606]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF__Location__UTCDat__145C0A3F]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[Location] ADD  CONSTRAINT [DF__Location__UTCDat__15502E78]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[Manufacturer] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Manufacturer] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Manufacturer] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MaturityDef] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[MaturityDef] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[MaturityDef] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MechAction] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[MechAction] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[MechAction] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Model] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Model] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Model] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [ProgStatus]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [ActualValue]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [NearEndUsage]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [AvgDailyUsage]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [WeeklyFlightDays]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[Program] ADD  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__IsActive__1FCDBCEB]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__IsDeleted__20C1E124]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__UTCDateCr__21B6055D]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF__Route__UTCDateMo__22AA2996]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[Mechanic] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Mechanic] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Mechanic] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Region] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Test] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LoadingConfig] ADD  CONSTRAINT [DF_LoadingConfig_SortOrder]  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Propeller] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[Propeller] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[Propeller] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[PropellerType] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[PropellerType] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[PropellerType] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF_Flight_LoadingConfigId]  DEFAULT ((0)) FOR [LoadingConfigId]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__TakeoffW__2E1BDC42]  DEFAULT ((0)) FOR [TakeoffWeight]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__PreFlightF__2F10007B]  DEFAULT ((0)) FOR [PreFlightFtiOK]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__PreFlightT__300424B4]  DEFAULT ((0)) FOR [PreFlightTelemetryOK]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__PreFlightT__30F848ED]  DEFAULT ((0)) FOR [PreFlightTelemetryCheckComplete]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__OilLevelIn__31EC6D26]  DEFAULT ((0)) FOR [OilLevelIndicatorStart]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__HydraulicLevelI__32E0915F]  DEFAULT ((0)) FOR [HydraulicLevelIndicatorStart]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__UsedMeterO__33D4B598]  DEFAULT ((0)) FOR [UsedMeterOffset]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__EngineHour__34C8D9D1]  DEFAULT ((0)) FOR [EngineHoursStart]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__HobbsSt__35BCFE0A]  DEFAULT ((0)) FOR [HobbsStart]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__HobbsEn__36B12243]  DEFAULT ((0)) FOR [HobbsEnd]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__EngineHour__37A5467C]  DEFAULT ((0)) FOR [EngineHoursEnd]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__PostFlight__3C69FB99]  DEFAULT ((0)) FOR [PostFlightFtiOK]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__Nacelle__3D5E1FD2]  DEFAULT ((0)) FOR [NacelleCheckBuildup]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__Nacelle__3E52440B]  DEFAULT ((0)) FOR [NacelleCheckDiscoloration]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__IsActive__3F466844]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__IsDeleted__403A8C7D]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__UTCDateCre__412EB0B6]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[Flight] ADD  CONSTRAINT [DF__Flight__UTCDateMod__4222D4EF]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [AirframeWeightID]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [Cargo1WeightId]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [Cargo2WeightId]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [BallastWeightID]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [AirframeHoursStart]
GO
ALTER TABLE [dbo].[Flight] ADD  DEFAULT ((0)) FOR [AirframeHoursEnd]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF__FlightComme__Compo__5441852A]  DEFAULT ((0)) FOR [ComponentId]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF_FlightComment_FlightCommentGUID]  DEFAULT (newid()) FOR [FlightCommentGUID]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF__FlightComme__IsAct__5629CD9C]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF__FlightComme__IsDel__571DF1D5]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF__FlightComme__UTCDa__5812160E]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FlightComment] ADD  CONSTRAINT [DF__FlightComme__UTCDa__59063A47]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[FlightComment] ADD  DEFAULT ((0)) FOR [IssueCodeId]
GO
ALTER TABLE [dbo].[FlightEngineRun] ADD  CONSTRAINT [DF_FlightEngineRun_FlightEngineRunGuid]  DEFAULT (newid()) FOR [FlightEngineRunGuid]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF__FlightRegion__Hours__619B8048]  DEFAULT ((0)) FOR [Hours]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF_FlightRegionTime_FlightRegionTimeGUID]  DEFAULT (newid()) FOR [FlightRegionTimeGUID]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF__FlightRegion__IsAct__628FA481]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF__FlightRegion__IsDel__6383C8BA]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF__FlightRegion__UTCDa__6477ECF3]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[FlightRegionTime] ADD  CONSTRAINT [DF__FlightRegion__UTCDa__656C112C]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[UTCTimeZone] ADD  CONSTRAINT [DF__UTCTimeZo__IsAct__7B5B524B]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Aircraft] ADD  CONSTRAINT [DF__Aircraft__Airfram__08EA5793]  DEFAULT ((0)) FOR [AirframeHoursStart]
GO
ALTER TABLE [dbo].[Aircraft] ADD  CONSTRAINT [DF__Aircraft__IsActiv__0BC6C43E]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Aircraft] ADD  CONSTRAINT [DF__Aircraft__IsDelet__0CBAE877]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Aircraft] ADD  CONSTRAINT [DF__Aircraft__UTCDate__0DAF0CB0]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[Aircraft] ADD  CONSTRAINT [DF__Aircraft__UTCDate__0EA330E9]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[Aircraft] ADD  DEFAULT ((0)) FOR [AirframeHoursCurrent]
GO
ALTER TABLE [dbo].[Aircraft] ADD  DEFAULT ((0)) FOR [AirframeHoursOffset]
GO
ALTER TABLE [dbo].[AircraftComment] ADD  CONSTRAINT [DF_AircraftComment_FlightCommentGUID]  DEFAULT (newid()) FOR [AircraftCommentGUID]
GO
ALTER TABLE [dbo].[AircraftComment] ADD  DEFAULT ((0)) FOR [IssueCodeId]
GO
ALTER TABLE [dbo].[AircraftComponent] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[AircraftComponent] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[AircraftComponent] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__Confi__681373AD]  DEFAULT ((0)) FOR [ConfigurationMeterStart]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__Confi__690797E6]  DEFAULT ((0)) FOR [ConfigurationMeterEnd]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF_AircraftConfiguration_AircraftConfigurationGUID]  DEFAULT (newid()) FOR [AircraftConfigurationGUID]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__IsAct__69FBBC1F]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__IsDel__6AEFE058]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__UTCDa__6BE40491]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[AircraftConfiguration] ADD  CONSTRAINT [DF__AircraftCo__UTCDa__6CD828CA]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__Requi__25869641]  DEFAULT ((0)) FOR [RequiredUsage]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__Accum__267ABA7A]  DEFAULT ((0)) FOR [AccumUsage]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__LastL__276EDEB3]  DEFAULT ((0)) FOR [LastLocation]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF_AircraftLocation_AircraftLocationGUID]  DEFAULT (newid()) FOR [AircraftLocationGUID]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__IsAct__286302EC]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__IsDel__29572725]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__UTCDa__2A4B4B5E]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[AircraftLocation] ADD  CONSTRAINT [DF__AircraftLo__UTCDa__2B3F6F97]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[AircraftStatusDef] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[AircraftStatusDef] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[AircraftStatusDef] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WeightDef] ADD  CONSTRAINT [DF_WeightDef_WeightDefGUID]  DEFAULT (newid()) FOR [WeightDefGUID]
GO
ALTER TABLE [dbo].[WOEngineerComment] ADD  CONSTRAINT [DF_WOEngineerComment_EngineerCommentGUID]  DEFAULT (newid()) FOR [EngineerCommentGUID]
GO
ALTER TABLE [dbo].[WOEngineerComment] ADD  CONSTRAINT [DF__WOEnginee__IsDel__5AB9788F]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WOEngineerComment] ADD  CONSTRAINT [DF__WOEnginee__UTCDa__5BAD9CC8]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WOEngineerComment] ADD  CONSTRAINT [DF__WOEnginee__UTCDa__5CA1C101]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WOEngineerComment] ADD  DEFAULT ((0)) FOR [IssueCodeId]
GO
ALTER TABLE [dbo].[WOInstruction] ADD  CONSTRAINT [DF_WOInstruction_WOInstructionGUID]  DEFAULT (newid()) FOR [WOInstructionGUID]
GO
ALTER TABLE [dbo].[WOInstruction] ADD  CONSTRAINT [DF__WOInstruc__IsDel__70A8B9AE]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WOInstruction] ADD  CONSTRAINT [DF__WOInstruc__UTCDa__719CDDE7]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WOInstruction] ADD  CONSTRAINT [DF__WOInstruc__UTCDa__72910220]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WOPerformedWork] ADD  CONSTRAINT [DF_WOPerformedWork_WOPerformedWorkGUID]  DEFAULT (newid()) FOR [WOPerformedWorkGUID]
GO
ALTER TABLE [dbo].[WOPerformedWork] ADD  CONSTRAINT [DF__WOPerform__IsDel__76619304]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WOPerformedWork] ADD  CONSTRAINT [DF__WOPerform__UTCDa__7755B73D]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WOPerformedWork] ADD  CONSTRAINT [DF__WOPerform__UTCDa__7849DB76]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WOReason] ADD  CONSTRAINT [DF_WOReason_WOReasonGUID]  DEFAULT (newid()) FOR [WOReasonGUID]
GO
ALTER TABLE [dbo].[WOReason] ADD  CONSTRAINT [DF_WOReason_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF__WorkOrder__Meter__68487DD7]  DEFAULT ((0)) FOR [MeterEndWork]
GO
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF__WorkOrder__IsDel__6A30C649]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF__WorkOrder__UTCDa__6B24EA82]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WorkOrder] ADD  CONSTRAINT [DF__WorkOrder__UTCDa__6C190EBB]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WorkOrderDocXref] ADD  CONSTRAINT [DF__WorkOrder__IsDel__65370702]  DEFAULT ((1)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WorkOrderPart] ADD  CONSTRAINT [DF_WorkOrderPart_WorkOrderPartGUID]  DEFAULT (newid()) FOR [WorkOrderPartGUID]
GO
ALTER TABLE [dbo].[WorkOrderPart] ADD  CONSTRAINT [DF__WorkOrder__IsDel__6FE99F9F]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WorkOrderPart] ADD  CONSTRAINT [DF__WorkOrder__UTCDa__70DDC3D8]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WorkOrderPart] ADD  CONSTRAINT [DF__WorkOrder__UTCDa__71D1E811]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WorkOrderStatus] ADD  CONSTRAINT [DF_WorkOrderStatus_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkOrderType] ADD  CONSTRAINT [DF_WorkOrderType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WorkShift] ADD  DEFAULT ((0)) FOR [ProgId]
GO
ALTER TABLE [dbo].[WorkShift] ADD  DEFAULT ((0)) FOR [SortOrder]
GO
ALTER TABLE [dbo].[WorkShift] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[WOMechanicComment] ADD  CONSTRAINT [DF_WOMechanicComment_WOMechanicCommentGUID]  DEFAULT (newid()) FOR [WOMechanicCommentGUID]
GO
ALTER TABLE [dbo].[WOMechanicComment] ADD  CONSTRAINT [DF__WOMechani__IsDel__607251E5]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[WOMechanicComment] ADD  CONSTRAINT [DF__WOMechani__UTCDa__6166761E]  DEFAULT (getutcdate()) FOR [UTCDateCreated]
GO
ALTER TABLE [dbo].[WOMechanicComment] ADD  CONSTRAINT [DF__WOMechani__UTCDa__625A9A57]  DEFAULT (getutcdate()) FOR [UTCDateModified]
GO
ALTER TABLE [dbo].[WOMechanicComment] ADD  DEFAULT ((0)) FOR [IssueCodeId]
GO
ALTER TABLE [dbo].[APUType]  WITH CHECK ADD  CONSTRAINT [FK_APUType_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[APUType] CHECK CONSTRAINT [FK_APUType_Program]
GO
ALTER TABLE [dbo].[FltRole]  WITH CHECK ADD  CONSTRAINT [FK_FltRole_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[FltRole] CHECK CONSTRAINT [FK_FltRole_Program]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions]  WITH CHECK ADD  CONSTRAINT [FK_FltRoleModulePermissions_FltModule] FOREIGN KEY([ModuleName])
REFERENCES [dbo].[FltModule] ([ModuleName])
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] CHECK CONSTRAINT [FK_FltRoleModulePermissions_FltModule]
GO
ALTER TABLE [dbo].[FltRoleModulePermissions]  WITH CHECK ADD  CONSTRAINT [FK_FltRoleModulePermissions_FltRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[FltRole] ([RoleId])
GO
ALTER TABLE [dbo].[FltRoleModulePermissions] CHECK CONSTRAINT [FK_FltRoleModulePermissions_FltRole]
GO
ALTER TABLE [dbo].[FltUserInRole]  WITH CHECK ADD  CONSTRAINT [FK_FltUserInRole_FltRole] FOREIGN KEY([RoleId])
REFERENCES [dbo].[FltRole] ([RoleId])
GO
ALTER TABLE [dbo].[FltUserInRole] CHECK CONSTRAINT [FK_FltUserInRole_FltRole]
GO
ALTER TABLE [dbo].[FltUserInRole]  WITH CHECK ADD  CONSTRAINT [FK_FltUserInRole_FltUser] FOREIGN KEY([UserName])
REFERENCES [dbo].[FltUser] ([UserName])
GO
ALTER TABLE [dbo].[FltUserInRole] CHECK CONSTRAINT [FK_FltUserInRole_FltUser]
GO
ALTER TABLE [dbo].[DocumentInfo]  WITH CHECK ADD  CONSTRAINT [FK_Document_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[DocumentInfo] CHECK CONSTRAINT [FK_Document_Program]
GO
ALTER TABLE [dbo].[Pilot]  WITH CHECK ADD  CONSTRAINT [FK_Pilot_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Pilot] CHECK CONSTRAINT [FK_Pilot_Program]
GO
ALTER TABLE [dbo].[Emission]  WITH CHECK ADD  CONSTRAINT [FK_Emission_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Emission] CHECK CONSTRAINT [FK_Emission_Program]
GO
ALTER TABLE [dbo].[Engineer]  WITH CHECK ADD  CONSTRAINT [FK_Engineer_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Engineer] CHECK CONSTRAINT [FK_Engineer_Program]
GO
ALTER TABLE [dbo].[EngineType]  WITH CHECK ADD  CONSTRAINT [FK_EngineType_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[EngineType] CHECK CONSTRAINT [FK_EngineType_Program]
GO
ALTER TABLE [dbo].[IssueCode]  WITH CHECK ADD  CONSTRAINT [FK_IssueCode_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[IssueCode] CHECK CONSTRAINT [FK_IssueCode_Program]
GO
ALTER TABLE [dbo].[LengthUnit]  WITH CHECK ADD  CONSTRAINT [FK_LengthUnit_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[LengthUnit] CHECK CONSTRAINT [FK_LengthUnit_Program]
GO
ALTER TABLE [dbo].[LiquidFill]  WITH CHECK ADD  CONSTRAINT [FK_LiquidFill_LiquidGroup] FOREIGN KEY([LiquidGroupId])
REFERENCES [dbo].[LiquidGroup] ([ID])
GO
ALTER TABLE [dbo].[LiquidFill] CHECK CONSTRAINT [FK_LiquidFill_LiquidGroup]
GO
ALTER TABLE [dbo].[LiquidFill]  WITH CHECK ADD  CONSTRAINT [FK_LiquidFill_LiquidType] FOREIGN KEY([LiquidTypeId])
REFERENCES [dbo].[LiquidType] ([Id])
GO
ALTER TABLE [dbo].[LiquidFill] CHECK CONSTRAINT [FK_LiquidFill_LiquidType]
GO
ALTER TABLE [dbo].[LiquidFill]  WITH CHECK ADD  CONSTRAINT [FK_LiquidFill_LiquidUnit] FOREIGN KEY([LiquidUnitId])
REFERENCES [dbo].[LiquidUnit] ([Id])
GO
ALTER TABLE [dbo].[LiquidFill] CHECK CONSTRAINT [FK_LiquidFill_LiquidUnit]
GO
ALTER TABLE [dbo].[LiquidFill]  WITH CHECK ADD  CONSTRAINT [FK_LiquidFill_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([FlightId])
GO
ALTER TABLE [dbo].[LiquidFill] CHECK CONSTRAINT [FK_LiquidFill_Flight]
GO
ALTER TABLE [dbo].[LiquidGroup]  WITH CHECK ADD  CONSTRAINT [FK_LiquidGroup_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[LiquidGroup] CHECK CONSTRAINT [FK_LiquidGroup_Program]
GO
ALTER TABLE [dbo].[LiquidType]  WITH CHECK ADD  CONSTRAINT [FK_LiquidType_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[LiquidType] CHECK CONSTRAINT [FK_LiquidType_Program]
GO
ALTER TABLE [dbo].[LiquidUnit]  WITH CHECK ADD  CONSTRAINT [FK_LiquidUnit_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[LiquidUnit] CHECK CONSTRAINT [FK_LiquidUnit_Program]
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD  CONSTRAINT [FK_Location_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Location] CHECK CONSTRAINT [FK_Location_Program]
GO
ALTER TABLE [dbo].[Manufacturer]  WITH CHECK ADD  CONSTRAINT [FK_Manufacturer_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Manufacturer] CHECK CONSTRAINT [FK_Manufacturer_Program]
GO
ALTER TABLE [dbo].[MaturityDef]  WITH CHECK ADD  CONSTRAINT [FK_MaturityDef_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[MaturityDef] CHECK CONSTRAINT [FK_MaturityDef_Program]
GO
ALTER TABLE [dbo].[MechAction]  WITH CHECK ADD  CONSTRAINT [FK_MechAction_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[MechAction] CHECK CONSTRAINT [FK_MechAction_Program]
GO
ALTER TABLE [dbo].[Model]  WITH CHECK ADD  CONSTRAINT [FK_Model_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Model] CHECK CONSTRAINT [FK_Model_Program]
GO
ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_Route_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocactionId])
GO
ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_Route_Location]
GO
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_Test_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_Test_Program]
GO
ALTER TABLE [dbo].[Propeller]  WITH CHECK ADD  CONSTRAINT [FK_Propeller_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Propeller] CHECK CONSTRAINT [FK_Propeller_Program]
GO
ALTER TABLE [dbo].[PropellerType]  WITH CHECK ADD  CONSTRAINT [FK_PropellerType_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[PropellerType] CHECK CONSTRAINT [FK_PropellerType_Program]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocactionId])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Location]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Route] FOREIGN KEY([SchedRouteId])
REFERENCES [dbo].[Route] ([RouteId])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Route]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftId])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_Aircraft]
GO
ALTER TABLE [dbo].[Flight]  WITH CHECK ADD  CONSTRAINT [FK_Flight_WorkShift] FOREIGN KEY([WorkShiftIndicatorId])
REFERENCES [dbo].[WorkShift] ([Id])
GO
ALTER TABLE [dbo].[Flight] CHECK CONSTRAINT [FK_Flight_WorkShift]
GO
ALTER TABLE [dbo].[FlightComment]  WITH CHECK ADD  CONSTRAINT [FK_FlightComment_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([FlightId])
GO
ALTER TABLE [dbo].[FlightComment] CHECK CONSTRAINT [FK_FlightComment_Flight]
GO
ALTER TABLE [dbo].[FlightComment]  WITH CHECK ADD  CONSTRAINT [FK_FlightComment_AircraftComponent] FOREIGN KEY([ComponentId])
REFERENCES [dbo].[AircraftComponent] ([Id])
GO
ALTER TABLE [dbo].[FlightComment] CHECK CONSTRAINT [FK_FlightComment_AircraftComponent]
GO
ALTER TABLE [dbo].[FlightEngineRun]  WITH CHECK ADD  CONSTRAINT [FK_FlightEngineRun_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([FlightId])
GO
ALTER TABLE [dbo].[FlightEngineRun] CHECK CONSTRAINT [FK_FlightEngineRun_Flight]
GO
ALTER TABLE [dbo].[FlightRegionTime]  WITH CHECK ADD  CONSTRAINT [FK_FlightRegionTime_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[FlightRegionTime] CHECK CONSTRAINT [FK_FlightRegionTime_Region]
GO
ALTER TABLE [dbo].[FlightRegionTime]  WITH CHECK ADD  CONSTRAINT [FK_FlightRegionTime_Flight] FOREIGN KEY([FlightId])
REFERENCES [dbo].[Flight] ([FlightId])
GO
ALTER TABLE [dbo].[FlightRegionTime] CHECK CONSTRAINT [FK_FlightRegionTime_Flight]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_Engineer] FOREIGN KEY([EngineerId])
REFERENCES [dbo].[Engineer] ([Id])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_Engineer]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_Manufacturer] FOREIGN KEY([ManufacturerId])
REFERENCES [dbo].[Manufacturer] ([Id])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_Manufacturer]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_Model] FOREIGN KEY([ModelId])
REFERENCES [dbo].[Model] ([Id])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_Model]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_Program]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_Mechanic] FOREIGN KEY([MechanicId])
REFERENCES [dbo].[Mechanic] ([Id])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_Mechanic]
GO
ALTER TABLE [dbo].[Aircraft]  WITH CHECK ADD  CONSTRAINT [FK_Aircraft_AircraftStatusDef] FOREIGN KEY([AircraftStatusId])
REFERENCES [dbo].[AircraftStatusDef] ([Id])
GO
ALTER TABLE [dbo].[Aircraft] CHECK CONSTRAINT [FK_Aircraft_AircraftStatusDef]
GO
ALTER TABLE [dbo].[AircraftComment]  WITH CHECK ADD  CONSTRAINT [FK_AircraftComment_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftId])
GO
ALTER TABLE [dbo].[AircraftComment] CHECK CONSTRAINT [FK_AircraftComment_Aircraft]
GO
ALTER TABLE [dbo].[AircraftComponent]  WITH CHECK ADD  CONSTRAINT [FK_AircraftComponent_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[AircraftComponent] CHECK CONSTRAINT [FK_AircraftComponent_Program]
GO
ALTER TABLE [dbo].[AircraftConfiguration]  WITH CHECK ADD  CONSTRAINT [FK_AircraftConfiguration_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftId])
GO
ALTER TABLE [dbo].[AircraftConfiguration] CHECK CONSTRAINT [FK_AircraftConfiguration_Aircraft]
GO
ALTER TABLE [dbo].[AircraftLocation]  WITH CHECK ADD  CONSTRAINT [FK_AircraftLocation_Location] FOREIGN KEY([LocationId])
REFERENCES [dbo].[Location] ([LocactionId])
GO
ALTER TABLE [dbo].[AircraftLocation] CHECK CONSTRAINT [FK_AircraftLocation_Location]
GO
ALTER TABLE [dbo].[AircraftLocation]  WITH CHECK ADD  CONSTRAINT [FK_AircraftLocation_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftId])
GO
ALTER TABLE [dbo].[AircraftLocation] CHECK CONSTRAINT [FK_AircraftLocation_Aircraft]
GO
ALTER TABLE [dbo].[AircraftStatusDef]  WITH CHECK ADD  CONSTRAINT [FK_AircraftStatusDef_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[AircraftStatusDef] CHECK CONSTRAINT [FK_AircraftStatusDef_Program]
GO
ALTER TABLE [dbo].[WOEngineerComment]  WITH CHECK ADD  CONSTRAINT [FK_WOEngineerComment_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WOEngineerComment] CHECK CONSTRAINT [FK_WOEngineerComment_WorkOrder]
GO
ALTER TABLE [dbo].[WOInstruction]  WITH CHECK ADD  CONSTRAINT [FK_WOInstruction_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WOInstruction] CHECK CONSTRAINT [FK_WOInstruction_WorkOrder]
GO
ALTER TABLE [dbo].[WOPerformedWork]  WITH CHECK ADD  CONSTRAINT [FK_WOPerformedWork_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WOPerformedWork] CHECK CONSTRAINT [FK_WOPerformedWork_WorkOrder]
GO
ALTER TABLE [dbo].[WorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrder_Aircraft] FOREIGN KEY([AircraftId])
REFERENCES [dbo].[Aircraft] ([AircraftId])
GO
ALTER TABLE [dbo].[WorkOrder] CHECK CONSTRAINT [FK_WorkOrder_Aircraft]
GO
ALTER TABLE [dbo].[WorkOrderDocXref]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrderDocXref_Document] FOREIGN KEY([DocumentId])
REFERENCES [dbo].[DocumentInfo] ([DocumentId])
GO
ALTER TABLE [dbo].[WorkOrderDocXref] CHECK CONSTRAINT [FK_WorkOrderDocXref_Document]
GO
ALTER TABLE [dbo].[WorkOrderDocXref]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrderDocXref_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WorkOrderDocXref] CHECK CONSTRAINT [FK_WorkOrderDocXref_WorkOrder]
GO
ALTER TABLE [dbo].[WorkOrderPart]  WITH CHECK ADD  CONSTRAINT [FK_WorkOrderPart_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WorkOrderPart] CHECK CONSTRAINT [FK_WorkOrderPart_WorkOrder]
GO
ALTER TABLE [dbo].[WorkShift]  WITH CHECK ADD  CONSTRAINT [FK_WorkShift_Program] FOREIGN KEY([ProgId])
REFERENCES [dbo].[Program] ([ProgID])
GO
ALTER TABLE [dbo].[WorkShift] CHECK CONSTRAINT [FK_WorkShift_Program]
GO
ALTER TABLE [dbo].[WOMechanicComment]  WITH CHECK ADD  CONSTRAINT [FK_WOMechanicComment_WorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[WorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[WOMechanicComment] CHECK CONSTRAINT [FK_WOMechanicComment_WorkOrder]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAppSetting]
	@TimeZoneCode varchar(10),
	@SMTPServerAndPort varchar(255) = '',
	@SMTPAuthentication varchar(255) = '',
	@SMTPEnableSSL bit = 0,
	@SMTPUserName varchar(255) = '',
	@SMTPPassword varchar(255) = '',
	@StampUserName nvarchar(20), 
	@AppId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AppSetting] (
		TimeZoneCode,
		SMTPServerAndPort,
		SMTPAuthentication,
		SMTPEnableSSL,
		SMTPUserName,
		SMTPPassword,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@TimeZoneCode,
		@SMTPServerAndPort,
		@SMTPAuthentication,
		@SMTPEnableSSL,
		@SMTPUserName,
		@SMTPPassword,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @AppId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[AppSetting]
	WHERE
		AppId = @AppId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAPUType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[APUType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[APUType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFltModule]
	@ModuleName nvarchar(50),
	@ModuleDescription nvarchar(255) = '',
	@StampUserName nvarchar(20), 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FltModule] (
		ModuleName,
		ModuleDescription,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@ModuleName,
		@ModuleDescription,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FltModule]
	WHERE
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFltRole]
	@ProgId Int,
	@RoleName nvarchar(50),
	@RoleDescription nvarchar(255) = '',
	@IsProgramManager bit = 0,
	@StampUserName nvarchar(20), 
	@RoleId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FltRole] (
		ProgId,
		RoleName,
		RoleDescription,
		IsProgramManager,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@ProgId,
		@RoleName,
		@RoleDescription,
		@IsProgramManager,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @RoleId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FltRole]
	WHERE
		RoleId = @RoleId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFltRoleModulePermissions]
	@RoleId Int,
	@ModuleName nvarchar(50),
	@CanView bit = 0,
	@CanEdit bit = 0,
	@StampUserName nvarchar(20), 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FltRoleModulePermissions] (
		RoleId,
		ModuleName,
		CanView,
		CanEdit,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@RoleId,
		@ModuleName,
		@CanView,
		@CanEdit,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FltRoleModulePermissions]
	WHERE
		RoleId = @RoleId AND
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFltUser]
	@UserName nvarchar(20),
	@Password nvarchar(128),
	@PasswordSalt nvarchar(128) = '',
	@Email nvarchar(255),
	@PasswordQuestion nvarchar(255) = '',
	@PasswordAnswer nvarchar(128) = '',
	@IsSuperUser bit = 0,
	@IsApproved bit = 1,
	@IsLockedOut bit = 0,
	@utcLastLoginDate datetime = Null,
	@utcLastLockoutDate datetime = Null,
	@utcLastPasswordChangedDate datetime = Null,
	@FailedPasswordAttemptCount Int = 0,
	@utcFailedPasswordAttemptWindowStart datetime = Null,
	@FailedPasswordAnswerAttemptCount Int = 0,
	@utcFailedPasswordAnswerAttemptWindowStart datetime = Null,
	@DisplayName nvarchar(30) = '',
	@Location nvarchar(30) = '',
	@CountryCode nvarchar(20) = '',
	@PhoneNumber nvarchar(20) = '',
	@MgrUserName nvarchar(20) = '',
	@Department nvarchar(30) = '',
	@Title nvarchar(30) = '',
	@Company nvarchar(30) = '',
	@DefaultProgId Int = 0,
	@StampUserName nvarchar(20), 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FltUser] (
		UserName,
		Password,
		PasswordSalt,
		Email,
		PasswordQuestion,
		PasswordAnswer,
		IsSuperUser,
		IsApproved,
		IsLockedOut,
		utcLastLoginDate,
		utcLastLockoutDate,
		utcLastPasswordChangedDate,
		FailedPasswordAttemptCount,
		utcFailedPasswordAttemptWindowStart,
		FailedPasswordAnswerAttemptCount,
		utcFailedPasswordAnswerAttemptWindowStart,
		DisplayName,
		Location,
		CountryCode,
		PhoneNumber,
		MgrUserName,
		Department,
		Title,
		Company,
		DefaultProgId,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@UserName,
		@Password,
		@PasswordSalt,
		@Email,
		@PasswordQuestion,
		@PasswordAnswer,
		@IsSuperUser,
		@IsApproved,
		@IsLockedOut,
		@utcLastLoginDate,
		@utcLastLockoutDate,
		@utcLastPasswordChangedDate,
		@FailedPasswordAttemptCount,
		@utcFailedPasswordAttemptWindowStart,
		@FailedPasswordAnswerAttemptCount,
		@utcFailedPasswordAnswerAttemptWindowStart,
		@DisplayName,
		@Location,
		@CountryCode,
		@PhoneNumber,
		@MgrUserName,
		@Department,
		@Title,
		@Company,
		@DefaultProgId,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FltUser]
	WHERE
		UserName = @UserName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFltUserInRole]
	@UserName NVARCHAR(20),
	@RoleId INT,
	@StampUserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION
		
	UPDATE FltUserInRole
	SET	
		IsDeleted = 0,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @Stampusername
	WHERE
		UserName = @UserName AND
		RoleId = @RoleId

	IF @@ROWCOUNT = 0 
	BEGIN
		INSERT INTO FltUserInRole (
			UserName,
			RoleId,
			IsDeleted,
			UtcDateCreated,
			UtcDateModified,
			StampUserName
			)
		VALUES (
			@UserName,
			@RoleId,
			0,
			GETUTCDATE(),
			GETUTCDATE(),
			@StampUserName
			)
	END

	COMMIT TRANSACTION
		
END

SET ANSI_NULLS ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addDocType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[DocType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[DocType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addDocument]
	@DocumentId AS INT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION
	
	INSERT INTO [dbo].[Document] (
		DocumentId
		)
	VALUES (
		@DocumentId	
		)
	
	UPDATE dbo.Document
	SET DocumentContent = CONVERT(VARBINARY(MAX), 0)
	WHERE DocumentId = @DocumentId
	
	UPDATE dbo.Document
	SET DocumentContent .WRITE(Null, 0, 0)
	WHERE DocumentID = @DocumentId
	
	COMMIT TRANSACTION
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addDocumentInfo]
	@ProgId Int,
	@DocumentTypeId Int,
	@DocumentName varchar(255),
	@DocumentDescription nvarchar(255) = '',
	@DocumentSize INT,
	@StampUserName nvarchar(20), 
	@DocumentId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[DocumentInfo] (
		ProgId,
		DocumentTypeId,
		DocumentName,
		DocumentDescription,
		DocumentSize,
		UTCDateCreated,
		UTCDateModified,
		StampUserName
		)
	VALUES (
		@ProgId,
		@DocumentTypeId,
		@DocumentName,
		@DocumentDescription,
		@DocumentSize,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @DocumentId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[DocumentInfo]
	WHERE
		DocumentId = @DocumentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addDocumentType]
	@FileExtension VARCHAR(10),
	@MimeType VARCHAR(255),
	@DocumentTypeId INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[DocumentType] (
		FileExtension,
		MimeType
		)
	VALUES (
		@FileExtension,
		@MimeType
		)

	SELECT @DocumentTypeId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[DocumentType]
	WHERE
		DocumentTypeId = @DocumentTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addPilot]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Pilot] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Pilot]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addEmission]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Emission] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Emission]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addEngineer]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Engineer] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Engineer]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addEngineeringAction]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[EngineeringAction] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[EngineeringAction]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addEngineType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[EngineType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[EngineType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addIssueCode]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[IssueCode] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[IssueCode]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLengthUnit]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LengthUnit] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LengthUnit]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLiquidFill]
	@FlightId Int,
	@LiquidGroupId Int,
	@LiquidTypeId Int,
	@LiquidUnitId Int,
	@LiquidAmt DECIMAL(9,3),
	@HobbsReading DECIMAL(9,1),
	@AirframeHoursReading DECIMAL(9,1),
	@LiquidFillLocation nvarchar(255),
	@LiquidFillApprover NVARCHAR(30),
	@StampUserName nvarchar(20), 
	@LiquidFillId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LiquidFill] (
		FlightId,
		LiquidGroupId,
		LiquidTypeId,
		LiquidUnitId,
		LiquidAmt,
		HobbsReading,
		AirframeHoursReading,
		LiquidFillLocation,
		LiquidFillApprover,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@FlightId,
		@LiquidGroupId,
		@LiquidTypeId,
		@LiquidUnitId,
		@LiquidAmt,
		@HobbsReading,
		@AirframeHoursReading,
		@LiquidFillLocation,
		@LiquidFillApprover,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @LiquidFillId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[LiquidFill]
	WHERE
		LiquidFillId = @LiquidFillId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLiquidGroup]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@ID INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LiquidGroup] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @ID = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidGroup]
	WHERE
		ID = @ID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLiquidType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LiquidType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLiquidUnit]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LiquidUnit] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidUnit]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLocation]
	@ProgId Int,
	@TimeZoneId Int,
	@LocationName nvarchar(50),
	@StampUserName nvarchar(20), 
	@LocactionId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Location] (
		ProgId,
		TimeZoneId,
		LocationName,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@ProgId,
		@TimeZoneId,
		@LocationName,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @LocactionId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[Location]
	WHERE
		LocactionId = @LocactionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addManufacturer]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Manufacturer] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Manufacturer]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addMaturityDef]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[MaturityDef] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[MaturityDef]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addMechAction]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[MechAction] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[MechAction]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addModel]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Model] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Model]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addProgram]
	@ProgName nvarchar(50),
	@ProgDescription nvarchar(255) = '',
	@ProgVersion nvarchar(50) = '',
	@ProgramLogo nvarchar(255) = '',
	@ReportLogo nvarchar(255) = '',
	@YearLogo nvarchar(255) = '',
	@ProgStatus tinyint = 0,
	@DateOpened varchar(50) = '',
	@DateClosed varchar(50) = '',
	@ActualValue money = 0,
	@ProjectCode varchar(50) = '',
	@ActivityCode varchar(50) = '',
	@DeptCode varchar(50) = '',
	@CompanyName nvarchar(255) = '',
	@NearEndUsage Int = 0,
	@AvgDailyUsage Int = 0,
	@WeeklyFlightDays Int = 0,
	@StampUserName nvarchar(20), 
	@ProgID INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Program] (
		ProgName,
		ProgDescription,
		ProgVersion,
		ProgramLogo,
		ReportLogo,
		YearLogo,
		ProgStatus,
		DateOpened,
		DateClosed,
		ActualValue,
		ProjectCode,
		ActivityCode,
		DeptCode,
		CompanyName,
		NearEndUsage,
		AvgDailyUsage,
		WeeklyFlightDays,
		ProgramGuid,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@ProgName,
		@ProgDescription,
		@ProgVersion,
		@ProgramLogo,
		@ReportLogo,
		@YearLogo,
		@ProgStatus,
		@DateOpened,
		@DateClosed,
		@ActualValue,
		@ProjectCode,
		@ActivityCode,
		@DeptCode,
		@CompanyName,
		@NearEndUsage,
		@AvgDailyUsage,
		@WeeklyFlightDays,
		NEWID(),
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @ProgID = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[Program]
	WHERE
		ProgID = @ProgID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addRoute]
	@LocationId Int,
	@Name nvarchar(50),
	@Description nvarchar(255),
	@TimeZoneId Int,
	@StampUserName nvarchar(20), 
	@RouteId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Route] (
		LocationId,
		[Name],
		[Description],
		TimeZoneId,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@LocationId,
		@Name,
		@Description,
		@TimeZoneId,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @RouteId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[Route]
	WHERE
		RouteId = @RouteId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addRouteRegion]
	@RouteId Int,
	@RegionId Int,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[RouteRegion] (
		RouteId,
		RegionId,
		IsActive
		)
	VALUES (
		@RouteId,
		@RegionId,
		1
		)

	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[RouteRegion]
	WHERE
		RouteId = @RouteId AND
		RegionId = @RegionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addMechanic]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Mechanic] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Mechanic]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addRegion]
	@ProgId Int,
	@Code nvarchar(2),
	@Name nvarchar(255),
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Region] (
		ProgId,
		Code,
		Name,
		IsActive
		)
	VALUES (
		@ProgId,
		@Code,
		@Name,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Region]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addTest]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Test] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Test]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addLoadingConfig]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[LoadingConfig] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LoadingConfig]
	WHERE
		Id = @Id

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addPropeller]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Propeller] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Propeller]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addPropellerType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[PropellerType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[PropellerType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[addFlight]
	@AircraftId Int,
	@LocationId Int,
	@SchedRouteId Int,
	@FlownRouteId INT,
	@FlightDate varchar(10),
	@FlightDateSort VARCHAR(10),
	@WorkShiftIndicatorId Int,
	@AirframeWeightId INT = 0,
	@Cargo1WeightId INT = 0,
	@Cargo2WeightId INT = 0,
	@BallastWeightId INT = 0,
	@PilotId INT,
	@PreFlightFtiOK smallint = 0,
	@PreFlightTelemetryOK smallint = 0,
	@PreFlightTelemetryCheckComplete smallint = 0,
	@OilLevelIndicatorStart smallint = 0,
	@HydraulicLevelIndicatorStart smallint = 0,
	@UsedMeterOffset DECIMAL(9,1) = 0,
	@MeterUnitId INT,
	@DepartureTime varchar(8),
	@DepartureDateTime_Local DATETIME,
	@DepartureDateTime_UTC DATETIME,
	@AircraftEngineHoursCurrent DECIMAL(11,1) = 0,
	@EngineHoursStart DECIMAL(7,1) = 0,
	@HobbsStart DECIMAL(9,1),
	@AirframeHoursStart DECIMAL(9,1),
	@ReturnTime varchar(8),
	@ReturnDateTime_Local DATETIME,
	@ReturnDateTime_UTC DATETIME,
	@HobbsEnd DECIMAL(9,1),
	@AirframeHoursEnd DECIMAL(9,1),
	@EngineHoursEnd DECIMAL(7,1) = 0,
	@PostFlightFtiOK smallint = 0,
	@PostFlightTelemetryCheckComplete smallint = 0,
	@NacelleCheckBuildUp smallint = 0,
	@NacelleCheckDiscoloration smallint = 0,
	@IsApproved BIT = 0,
	@ApprovedBy NVARCHAR(20) = '',
	@UtcDateApproved DATETIME,
	@StampUserName nvarchar(20), 
	@FlightId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	IF (SELECT COUNT(*) FROM Flight 
		WHERE AircraftId = @AircraftId 
		AND LocationId = @LocationId 
		AND FlightDateSort = @FlightDateSort 
		AND WorkShiftIndicatorId = @WorkShiftIndicatorId
		AND IsDeleted = 0) <> 0 
	BEGIN
		RAISERROR('Duplicate Flight Entry',16,1)
	END

	INSERT INTO [dbo].[Flight] (
		AircraftId,
		LocationId,
		SchedRouteId,
		FlownRouteId,
		FlightDate,
		FlightDateSort,
		WorkShiftIndicatorId,
		AirframeWeightId,
		Cargo1WeightId,
		Cargo2WeightId,
		BallastWeightId,
		PilotId,
		PreFlightFtiOK,
		PreFlightTelemetryOK,
		PreFlightTelemetryCheckComplete,
		OilLevelIndicatorStart,
		HydraulicLevelIndicatorStart,
		UsedMeterOffset,
		MeterUnitId,
		DepartureTime,
		DepartureDateTime_Local,
		DepartureDateTime_UTC,
		AircraftEngineHoursCurrent,
		EngineHoursStart,
		HobbsStart,
		AirframeHoursStart,
		ReturnTime,
		ReturnDateTime_Local,
		ReturnDateTime_UTC,
		HobbsEnd,
		AirframeHoursEnd,
		EngineHoursEnd,
		PostFlightFtiOK,
		PostFlightTelemetryCheckComplete,
		NacelleCheckBuildup,
		NacelleCheckDiscoloration,
		IsApproved,
		ApprovedBy,
		UTCDateApproved,
		FlightGuid,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@AircraftId,
		@LocationId,
		@SchedRouteId,
		@FlownRouteId,
		@FlightDate,
		@FlightDateSort,
		@WorkShiftIndicatorId,
		@AirframeWeightId,
		@Cargo1WeightId,
		@Cargo2WeightId,
		@BallastWeightId,
		@PilotId,
		@PreFlightFtiOK,
		@PreFlightTelemetryOK,
		@PreFlightTelemetryCheckComplete,
		@OilLevelIndicatorStart,
		@HydraulicLevelIndicatorStart,
		@UsedMeterOffset,
		@MeterUnitId,
		@DepartureTime,
		@DepartureDateTime_Local,
		@DepartureDateTime_UTC,
		@AircraftEngineHoursCurrent,
		@EngineHoursStart,
		@HobbsStart,
		@AirframeHoursStart,
		@ReturnTime,
		@ReturnDateTime_Local,
		@ReturnDateTime_UTC,
		@HobbsEnd,
		@AirframeHoursEnd,
		@EngineHoursEnd,
		@PostFlightFtiOK,
		@PostFlightTelemetryCheckComplete,
		@NacelleCheckBuildUp,
		@NacelleCheckDiscoloration,
		@IsApproved,
		@ApprovedBy,
		@UtcDateApproved,
		NEWID(),
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @FlightId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[Flight]
	WHERE
		FlightId = @FlightId

	IF @IsApproved = 1
	BEGIN
		UPDATE dbo.Aircraft
			SET 
				Aircraft.StampUserName = @StampUserName,
				Aircraft.UTCDateModified = GETUTCDATE(),
				Aircraft.AirframeHoursCurrent = 
					(SELECT MAX(Flight.AirframeHoursEnd) FROM Flight 
					 WHERE Flight.AircraftId = @AircraftId
					 AND Flight.IsActive = 1 AND Flight.IsDeleted = 0),
				Aircraft.AircraftEngineHoursCurrent = 
					(SELECT MAX(Flight.EngineHoursEnd) FROM Flight
					WHERE Flight.AircraftId = @AircraftId
					AND Flight.IsActive = 1 AND Flight.IsDeleted = 0)
		WHERE dbo.Aircraft.AircraftId = @AircraftId
	END
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFlightComment]
	@FlightId Int,
	@ComponentId Int = 0,
	@CommentTime VARCHAR(8),
	@CommentDateTime_UTC DATETIME,
	@CommentDateTime_Local DATETIME,
	@IssueCodeId INT,
	@PilotComment nvarchar(2048),
	@CommentRating smallint,
	@StampUserName nvarchar(20), 
	@FlightCommentId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId timestamp OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FlightComment] (
		FlightId,
		ComponentId,
		CommentTime,
		CommentDateTime_UTC,
		CommentDateTime_Local,
		IssueCodeId,
		PilotComment,
		CommentRating,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@FlightId,
		@ComponentId,
		@CommentTime,
		@CommentDateTime_UTC,
		@CommentDateTime_Local,
		@IssueCodeId,
		@PilotComment,
		@CommentRating,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @FlightCommentId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FlightComment]
	WHERE
		FlightCommentId = @FlightCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFlightEngineRun]
	@FlightId Int,
	@RunStartTime VARCHAR(8),
	@RunEndTime VARCHAR(8),
	@EngineSpeed INT,
	@RunStartDateTime_Local DATETIME,
	@RunStartDateTime_UTC DATETIME,
	@RunEndDateTime_Local DATETIME,
	@RunEndDateTime_UTC DATETIME,
	@StampUserName nvarchar(20), 
	@FlightEngineRunId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId timestamp OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FlightEngineRun] (
		FlightId,
		RunStartTime,
		RunEndTime,
		EngineSpeed,
		RunStartDateTime_Local,
		RunStartDateTime_UTC,
		RunEndDateTime_Local,
		RunEndDateTime_UTC,
		IsDeleted,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@FlightId,
		@RunStartTime,
		@RunEndTime,
		@EngineSpeed,
		@RunStartDateTime_Local,
		@RunStartDateTime_UTC,
		@RunEndDateTime_Local,
		@RunEndDateTime_UTC,
		0,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @FlightEngineRunId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FlightEngineRun]
	WHERE
		FlightEngineRunId = @FlightEngineRunId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addFlightRegionTime]
	@FlightId Int,
	@RegionId Int,
	@Hours DECIMAL(7,1),
	@StampUserName nvarchar(20), 
	@FlightRegionTimeId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId timestamp OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[FlightRegionTime] (
		FlightId,
		RegionId,
		Hours,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@FlightId,
		@RegionId,
		@Hours,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @FlightRegionTimeId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[FlightRegionTime]
	WHERE
		FlightRegionTimeId = @FlightRegionTimeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addUTCTimeZone]
	@TimeZoneCode varchar(10),
	@TimeZoneOffset float,
	@TimeZoneDescr nvarchar(255),
	@DaylightSavingTime BIT,
	@DaylightSavingOffset FLOAT,
	@DaylightSavingBegin DATETIME = NULL,
	@DaylightSavingEnd DATETIME = NULL,
	@TimeZoneId INT OUTPUT,
	@NewConcurrencyId timestamp OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[UTCTimeZone] (
		TimeZoneCode,
		TimeZoneOffset,
		TimeZoneDescr,
		DaylightSavingTime,
		DaylightSavingOffset,
		DaylightSavingBegin,
		DaylightSavingEnd,
		IsActive
		)
	VALUES (
		@TimeZoneCode,
		@TimeZoneOffset,
		@TimeZoneDescr,
		@DaylightSavingTime,
		@DaylightSavingOffset,
		@DaylightSavingBegin,
		@DaylightSavingEnd,
		1
		)

	SELECT @TimeZoneId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[UTCTimeZone]
	WHERE
		TimeZoneId = @TimeZoneId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraft]
	@ProgId Int,
	@CurrentLocationId INT,
	@MSN varchar(20),
	@ShipNbr varchar(15),
	@SerialNbr varchar(6),
	@ModelId Int,
	@Description nvarchar(255),
	@EngineerId Int,
	@MechanicId Int,
	@RegistrationNbr varchar(10),
	@LiveryColor varchar(10),
	@AircraftStatusId Int,
	@ManufacturerId Int,
	@AirframeHoursStart DECIMAL(9,1),
	@AirframeHoursCurrent DECIMAL(9,1),
	@AirframeHoursOffset DECIMAL(9,1),
	@MeterUnitId INT,
	@TotalRequiredUsage DECIMAL(7,1) = 0,
	@AircraftEngineHoursCurrent DECIMAL(11,1) = 0,
	@EstEndOfTesting DATETIME = null,
	@ActEndOfTesting DATETIME = null,
	@StampUserName nvarchar(20), 
	@AircraftId INT OUTPUT,
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[Aircraft] (
		ProgId,
		CurrentLocationId,
		MSN,
		ShipNbr,
		SerialNbr,
		ModelId,
		[Description],
		EngineerId,
		MechanicId,
		RegistrationNbr,
		LiveryColor,
		AircraftStatusId,
		ManufacturerId,
		AirframeHoursStart,
		AirframeHoursCurrent,
		AirframeHoursOffset,
		MeterUnitId,
		TotalRequiredUsage,
		AircraftEngineHoursCurrent,
		EstEndOfTesting,
		ActEndOfTesting,
		AircraftGuid,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@ProgId,
		@CurrentLocationId,
		@MSN,
		@ShipNbr,
		@SerialNbr,
		@ModelId,
		@Description,
		@EngineerId,
		@MechanicId,
		@RegistrationNbr,
		@LiveryColor,
		@AircraftStatusId,
		@ManufacturerId,
		@AirframeHoursStart,
		@AirframeHoursCurrent,
		@AirframeHoursOffset,
		@MeterUnitId,
		@TotalRequiredUsage,
		@AircraftEngineHoursCurrent,
		@EstEndOfTesting,
		@ActEndOfTesting,
		NEWID(),
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @AircraftId = @@IDENTITY

	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[Aircraft]
	WHERE
		AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftComment]
	@AircraftId Int,
	@EngineerId INT,
	@ComponentId INT,
	@CommentDate DATETIME,
	@CommentRating SMALLINT,
	@IssueCodeId INT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@AircraftCommentId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AircraftComment] (
		AircraftId,
		EngineerId,
		ComponentId,
		CommentDate,
		CommentRating,
		IssueCodeId,
		Comments,
		IsDeleted,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 
		)
	VALUES (
		@AircraftId,
		@EngineerId,
		@ComponentId,
		@CommentDate,
		@CommentRating,
		@IssueCodeId,
		@Comments,
		0,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @AircraftCommentId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[AircraftComment]
	WHERE
		AircraftCommentId = @AircraftCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftComponent]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AircraftComponent] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[AircraftComponent]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftConfiguration]
	@AircraftId Int,
	@ConfigurationDescription nvarchar(255),
	@ConfigurationStartDate datetime,
	@ConfigurationEndDate DATETIME = null,
	@ConfigurationMeterStart DECIMAL(9,1) = 0,
	@ConfigurationMeterEnd DECIMAL(9,1) = 0,
	@ConfigurationMeterOffset DECIMAL(9,1) = 0,
	@MeterUnitId INT,
	@ModelId INT,
	@EngineTypeId Int,
	@EngineEmissionId Int,
	@EngineMaturityId Int,
	@EngineSerialNbr nvarchar(20),
	@APUTypeId Int,
	@APUEmissionId Int,
	@APUMaturityId Int,
	@APUSerialNbr nvarchar(30),
	@NoseGear nvarchar(30),
	@MainGear nvarchar(30),
	@PropellerTypeId Int,
	@PropellerModelNbr nvarchar(20),
	@PropellerSerialNbr nvarchar(20),
	@AvionicsVersion nvarchar(20),
	@EmissionLevel NVARCHAR(20),
	@StampUserName nvarchar(20), 
	@AircraftConfigurationId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AircraftConfiguration] (
		AircraftId,
		ConfigurationDescription,
		ConfigurationStartDate,
		ConfigurationEndDate,
		ConfigurationMeterStart,
		ConfigurationMeterEnd,
		ConfigurationMeterOffset,
		MeterUnitId,
		ModelId,
		EngineTypeId,
		EngineEmissionId,
		EngineMaturityId,
		EngineSerialNbr,
		APUTypeId,
		APUEmissionId,
		APUMaturityId,
		APUSerialNbr,
		NoseGear,
		MainGear,
		PropellerTypeId,
		PropellerModelNbr,
		PropellerSerialNbr,
		AvionicsVersion,
		EmissionLevel,
		IsDeleted,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@AircraftId,
		@ConfigurationDescription,
		@ConfigurationStartDate,
		@ConfigurationEndDate,
		@ConfigurationMeterStart,
		@ConfigurationMeterEnd,
		@ConfigurationMeterOffset,
		@MeterUnitId,
		@ModelId,
		@EngineTypeId,
		@EngineEmissionId,
		@EngineMaturityId,
		@EngineSerialNbr,
		@APUTypeId,
		@APUEmissionId,
		@APUMaturityId,
		@APUSerialNbr,
		@NoseGear,
		@MainGear,
		@PropellerTypeId,
		@PropellerModelNbr,
		@PropellerSerialNbr,
		@AvionicsVersion,
		@EmissionLevel,
		0,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @AircraftConfigurationId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[AircraftConfiguration]
	WHERE
		AircraftConfigurationId = @AircraftConfigurationId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftLocation]
	@AircraftId Int,
	@LocationId Int,
	@ArrivalDate DATETIME,
	@RequiredUsage DECIMAL(7,1),
	@AccumUsage DECIMAL(7,1),
	@EstEndOfTest DATETIME = null,
	@ActEndOfTest DATETIME = null,
	@LastLocation bit = 0,
	@StampUserName nvarchar(20), 
	@AircraftLocationId INT OUTPUT,
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AircraftLocation] (
		AircraftId,
		LocationId,
		ArrivalDate,
		RequiredUsage,
		AccumUsage,
		EstEndOfTest,
		ActEndOfTest,
		LastLocation,
		IsActive,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@AircraftId,
		@LocationId,
		@ArrivalDate,
		@RequiredUsage,
		@AccumUsage,
		@EstEndOfTest,
		@ActEndOfTest,
		@LastLocation,
		1,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @AircraftLocationId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[AircraftLocation]
	WHERE
		AircraftLocationId = @AircraftLocationId

	DECLARE @CurrentLocationId INTEGER
	SELECT @CurrentLocationId = ISNULL(LocationId, 0)
	FROM dbo.AircraftLocation
	WHERE AircraftLocationId = 
		(SELECT MAX(AircraftLocationId)
		 FROM AircraftLocation 
		 WHERE AircraftId = @AircraftId 
		 AND LastLocation = 1
		 AND IsActive = 1
		 AND IsDeleted = 0)
	
	UPDATE dbo.Aircraft
	SET 
		CurrentLocationId = @CurrentLocationId,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE AircraftId = @AircraftId
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftStatusDef]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[AircraftStatusDef] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[AircraftStatusDef]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addAircraftTestXref]
	@AircraftId INT,
	@TestId Int,
	@StampUserName NVARCHAR(20),
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId TIMESTAMP	OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION
	
	UPDATE [dbo].[AircraftTestXref]
	SET
		IsDeleted = 0,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE
		AircraftId = @AircraftId AND
		TestId = @TestId

	SELECT @NewUTCDate = UTCDateModified,
			@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[AircraftTestXref]
	WHERE [dbo].[AircraftTestXref].[AircraftId] = @AircraftId AND
		  [dbo].[AircraftTestXref].[TestId] = @TestId
	
	IF (@@ROWCOUNT = 0)
	BEGIN
		
		INSERT INTO [dbo].[AircraftTestXref] (
			AircraftId,
			TestId,
			IsDeleted,
			UTCDateCreated,
			UTCDateModified,
			StampUserName
			)
		VALUES (
			@AircraftId,
			@TestId,
			0,
			GETUTCDATE(),
			GETUTCDATE(),
			@StampUserName
			)

		SELECT @NewUTCDate = UTCDateCreated,
				@NewConcurrencyId = ConcurrencyId
		FROM [dbo].[AircraftTestXref]
		WHERE [dbo].[AircraftTestXref].[AircraftId] = @AircraftId AND
			  [dbo].[AircraftTestXref].[TestId] = @TestId
	
	END
	
	COMMIT TRANSACTION
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWeightDef]
	@ProgId INT,
	@WeightDefTypeId INT,
	@WeightDefDescr nvarchar(50),
	@Weight INT,
	@WeightUnitId INT,
	@SortOrder INT,
	@StampUserName NVARCHAR(20),
	@WeightDefId INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WeightDef] (
		ProgId,
		WeightDefTypeId,
		WeightDefDescr,
		[Weight],
		WeightUnitId,
		SortOrder,
		IsActive,
		IsDeleted,
		utcDateCreated,
		utcDateModified,
		StampUserName
		)
	VALUES (
		@ProgId,
		@WeightDefTypeid,
		@WeightDefDescr,
		@Weight,
		@WeightUnitId,
		@SortOrder,
		1,
		0,
		GETUTCDATE(),
		GETUTCDATE(),
		@StampUserName
		)

	SELECT @WeightDefId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightDef]
	WHERE
		WeightDefId = @WeightDefId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWeightDefType]
	@WeightDefTypeDescr nvarchar(50),
	@SortOrder Int = 0,
	@WeightDefTypeId INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WeightDefType] (
		WeightDefTypeDescr,
		SortOrder
		)
	VALUES (
		@WeightDefTypeDescr,
		@SortOrder
		)

	SELECT @WeightDefTypeId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightDefType]
	WHERE
		WeightDefTypeId = @WeightDefTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWeightUnit]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WeightUnit] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightUnit]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWOEngineerComment]
	@WorkOrderId Int,
	@ComponentId INT,
	@CommentDate DATETIME,
	@CommentRating SMALLINT,
	@IssueCodeId INT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@EngineerCommentsId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WOEngineerComment] (
		WorkOrderId,
		ComponentId,
		CommentDate,
		CommentRating,
		IssueCodeId,
		Comments,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@ComponentId,
		@CommentDate,
		@CommentRating,
		@IssueCodeId,
		@Comments,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @EngineerCommentsId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WOEngineerComment]
	WHERE
		EngineerCommentsId = @EngineerCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWOInstruction]
	@WorkOrderId Int,
	@InstructionLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@WOInstructionsId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WOInstruction] (
		WorkOrderId,
		InstructionLine,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@InstructionLine,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @WOInstructionsId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WOInstruction]
	WHERE
		WOInstructionsId = @WOInstructionsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWOPerformedWork]
	@WorkOrderId Int,
	@PerformedWorkLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@PerformedWorkId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WOPerformedWork] (
		WorkOrderId,
		PerformedWorkLine,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@PerformedWorkLine,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @PerformedWorkId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WOPerformedWork]
	WHERE
		PerformedWorkId = @PerformedWorkId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWOReason]
	@WorkOrderId Int,
	@ReasonLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@WOReasonId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WOReason] (
		WorkOrderId,
		ReasonLine,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@ReasonLine,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @WOReasonId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WOReason]
	WHERE
		WOReasonId = @WOReasonId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkOrder]
	@ProgId INT,
	@AircraftId Int,
	@LocationId Int,
	@WorkOrderTypeId Int,
	@WorkOrderStatusId INT,
	@EngineerId INT,
	@EngineeringActionId Int,
	@MechanicId Int,
	@RequestDate varchar(10),
	@FaultOccurredDate VARCHAR(10),
	@FaultOccurredMeter DECIMAL(8,1),
	@FISComplaintNbr nvarchar(8),
	@SquawkNbr nvarchar(20),
	@DueDate varchar(10),
	@DueTime varchar(8),
	@MeterStartWork DECIMAL(8,1),
	@FinishDate varchar(10),
	@FinishTime varchar(8),
	@MeterEndWork DECIMAL(8,1) = 0,
	@AircraftDispostion nvarchar(50),
	@AircraftIssueAlert BIT,
	@MaintenanceIssueAlert BIT,
	@StampUserName nvarchar(20), 
	@WorkOrderId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WorkOrder] (
		WorkOrderNbr,
		ProgId,
		AircraftId,
		LocationId,
		WorkOrderTypeId,
		WorkOrderStatusId,
		EngineerId,
		EngineeringActionId,
		MechanicId,
		RequestDate,
		FaultOccurredDate,
		FaultOccurredMeter,
		FISComplaintNbr,
		SquawkNbr,
		DueDate,
		DueTime,
		MeterStartWork,
		FinishDate,
		FinishTime,
		MeterEndWork,
		AircraftDispostion,
		AircraftIssueAlert,
		MaintenanceIssueAlert,
		WorkOrderGuid,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 
		)
	VALUES (
		'',
		@ProgId,
		@AircraftId,
		@LocationId,
		@WorkOrderTypeId,
		@WorkOrderStatusId,
		@EngineerId,
		@EngineeringActionId,
		@MechanicId,
		@RequestDate,
		@FaultOccurredDate,
		@FaultOccurredMeter,
		@FISComplaintNbr,
		@SquawkNbr,
		@DueDate,
		@DueTime,
		@MeterStartWork,
		@FinishDate,
		@FinishTime,
		@MeterEndWork,
		@AircraftDispostion,
		@AircraftIssueAlert,
		@MaintenanceIssueAlert,
		NEWID(),
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @WorkOrderId = @@IDENTITY
	
	DECLARE @WorkOrderNbr VARCHAR(10)
	SELECT @WorkOrderNbr = CONVERT(VARCHAR(10), @WorkOrderId)
	WHILE LEN(@WorkOrderNbr) < 10
	BEGIN
		SELECT @WorkOrderNbr = '0' + @WorkOrderNbr
	END
	
	UPDATE WorkOrder
	SET WorkOrderNbr = @WorkOrderNbr
	WHERE WorkOrderID = @WorkOrderId
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WorkOrder]
	WHERE
		WorkOrderId = @WorkOrderId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkOrderDocXref]
	@WorkOrderId Int,
	@DocumentId Int,
	@StampUserName NVARCHAR(20),
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId TIMESTAMP	OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	BEGIN TRANSACTION
	
	UPDATE [dbo].[WorkOrderDocXref]
	SET
		IsDeleted = 0,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE
		WorkOrderId = @WorkOrderId AND
		DocumentId = @DocumentId

	SELECT @NewUTCDate = UTCDateModified,
			@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkOrderDocXref]
	WHERE [dbo].[WorkOrderDocXref].[WorkOrderId] = @WorkOrderId AND
		  [dbo].[WorkOrderDocXref].[DocumentId] = @DocumentId
	
	IF (@@ROWCOUNT = 0)
	BEGIN
		
		INSERT INTO [dbo].[WorkOrderDocXref] (
			WorkOrderId,
			DocumentId,
			IsDeleted,
			UTCDateCreated,
			UTCDateModified,
			StampUserName
			)
		VALUES (
			@WorkOrderId,
			@DocumentId,
			0,
			GETUTCDATE(),
			GETUTCDATE(),
			@StampUserName
			)

		SELECT @NewUTCDate = UTCDateCreated,
				@NewConcurrencyId = ConcurrencyId
		FROM [dbo].[WorkOrderDocXref]
		WHERE [dbo].[WorkOrderDocXref].[WorkOrderId] = @WorkOrderId AND
			  [dbo].[WorkOrderDocXref].[DocumentId] = @DocumentId
	
	END
	
	COMMIT TRANSACTION

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkOrderPart]
	@WorkOrderId Int,
	@PartName nvarchar(20),
	@PartDescription nvarchar(255),
	@FisIDRemoved nvarchar(20),
	@PartNbrFailed Int,
	@FisIDAdded nvarchar(20),
	@PartModule nvarchar(20),
	@PartNbrRemoved Int,
	@PartNbrAdded Int,
	@StampUserName nvarchar(20), 
	@WorkOrderPartId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WorkOrderPart] (
		WorkOrderId,
		PartName,
		PartDescription,
		FisIDRemoved,
		PartNbrFailed,
		FisIDAdded,
		PartModule,
		PartNbrRemoved,
		PartNbrAdded,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@PartName,
		@PartDescription,
		@FisIDRemoved,
		@PartNbrFailed,
		@FisIDAdded,
		@PartModule,
		@PartNbrRemoved,
		@PartNbrAdded,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @WorkOrderPartId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WorkOrderPart]
	WHERE
		WorkOrderPartId = @WorkOrderPartId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkOrderStatus]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WorkOrderStatus] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkOrderStatus]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkOrderType]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WorkOrderType] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkOrderType]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWorkShift]
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@Id INT OUTPUT, 
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WorkShift] (
		ProgId,
		StringValue,
		SortOrder,
		IsActive
		)
	VALUES (
		@ProgId,
		@StringValue,
		@SortOrder,
		1
		)

	SELECT @Id = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkShift]
	WHERE
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[addWOMechanicComment]
	@WorkOrderId Int,
	@ComponentId INT,
	@CommentDate DATETIME,
	@CommentRating SMALLINT,
	@IssueCodeId INT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@WOMechanicCommentsId INT OUTPUT, 
	@NewUTCDate datetime OUTPUT,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	INSERT INTO [dbo].[WOMechanicComment] (
		WorkOrderId,
		ComponentId,
		CommentDate,
		CommentRating,
		IssueCodeId,
		Comments,
		UTCDateCreated,
		UTCDateModified,
		StampUserName 

		)
	VALUES (
		@WorkOrderId,
		@ComponentId,
		@CommentDate,
		@CommentRating,
		@IssueCodeId,
		@Comments,
		GetUTCDate(),
		GetUTCDate(),
		@StampUserName
		)

	SELECT @WOMechanicCommentsId = @@IDENTITY
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateCreated
	FROM [dbo].[WOMechanicComment]
	WHERE
		WOMechanicCommentsId = @WOMechanicCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[appendDocumentChunk]
	@DocumentId AS INT,
	@Chunk AS VARBINARY(MAX),
	@Offset AS INT = NULL,
	@ChunkLength AS INT = NULL
AS
BEGIN
	SET NOCOUNT ON

	UPDATE dbo.document
	SET documentcontent .WRITE(@Chunk, @Offset, @ChunkLength)
	WHERE DocumentId = @DocumentId
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAppSetting]
	@AppId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AppSetting]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		AppId = @AppId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAPUType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[APUType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFltModule]
	@ModuleName nvarchar(50), 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltModule]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFltRole]
	@RoleId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltRole]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		RoleId = @RoleId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFltRoleModulePermissions]
	@RoleId Int, 
	@ModuleName nvarchar(50), 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltRoleModulePermissions]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		RoleId = @RoleId AND
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFltUser]
	@UserName nvarchar(20), 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltUser]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		UserName = @UserName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFltUserInRole]
	@UserName NVARCHAR(20),
	@RoleId INT,
	@StampUserName NVARCHAR(20)
AS
BEGIN
	
	SET NOCOUNT ON

	UPDATE FltUserInRole
	SET
		IsDeleted = 1,
		UtcDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE
		UserName = @UserName AND
		RoleId = @RoleId
		
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteDocType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[DocType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteDocument]
	@DocumentId AS INT
AS
BEGIN
	SET NOCOUNT ON

	DELETE dbo.document WHERE dbo.document.documentId = @DocumentId
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteDocumentInfo]
	@DocumentId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[DocumentInfo]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		DocumentId = @DocumentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteDocumentType]
	@DocumentTypeId int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[DocumentType]
	WHERE 
		DocumentTypeId = @DocumentTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deletePilot]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Pilot]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteEmission]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Emission]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteEngineer]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Engineer]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteEngineeringAction]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[EngineeringAction]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteEngineType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[EngineType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteIssueCode]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[IssueCode]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLengthUnit]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[LengthUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLiquidFill]
	@LiquidFillId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LiquidFill]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		LiquidFillId = @LiquidFillId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLiquidGroup]
	@ID int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[LiquidGroup]
	WHERE 
		ID = @ID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLiquidType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[LiquidType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLiquidUnit]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[LiquidUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLocation]
	@LocactionId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Location]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		LocactionId = @LocactionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteManufacturer]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Manufacturer]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteMaturityDef]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[MaturityDef]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteMechAction]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[MechAction]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteModel]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Model]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteProgram]
	@ProgID int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Program]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		ProgID = @ProgID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteRoute]
	@RouteId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Route]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		RouteId = @RouteId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteRouteRegion]
	@RouteId Int, 
	@RegionId Int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[RouteRegion]
	WHERE 
		RouteId = @RouteId AND
		RegionId = @RegionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteMechanic]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Mechanic]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteRegion]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Region]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteTest]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Test]
	WHERE 
		Id = @Id
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteLoadingConfig]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[LoadingConfig]
	WHERE 
		Id = @Id

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deletePropeller]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[Propeller]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deletePropellerType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[PropellerType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFlight]
	@FlightId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @AircraftId INT
	
	SELECT @AircraftId = Flight.AircraftId
	FROM Flight 
	WHERE Flight.FlightId = @FlightId
	
	UPDATE [dbo].[Flight]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		FlightId = @FlightId

	UPDATE dbo.Aircraft
		SET 
			Aircraft.StampUserName = @StampUserName,
			Aircraft.UTCDateModified = GETUTCDATE(),
			Aircraft.AirframeHoursCurrent = 
				IsNull((SELECT MAX(Flight.AirframeHoursEnd) FROM Flight 
				 WHERE Flight.AircraftId = @AircraftId
				 AND Flight.IsActive = 1 AND Flight.IsDeleted = 0), 0)
	WHERE dbo.Aircraft.AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFlightComment]
	@FlightCommentId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightComment]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		FlightCommentId = @FlightCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFlightEngineRun]
	@FlightEngineRunId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightEngineRun]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		FlightEngineRunId = @FlightEngineRunId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteFlightRegionTime]
	@FlightRegionTimeId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightRegionTime]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		FlightRegionTimeId = @FlightRegionTimeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteUTCTimeZone]
	@TimeZoneId Int
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[UTCTimeZone]
	WHERE 
		TimeZoneId = @TimeZoneId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraft]
	@AircraftId Int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Aircraft]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftComment]
	@AircraftCommentId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftComment]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		AircraftCommentId = @AircraftCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftComponent]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[AircraftComponent]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftConfiguration]
	@AircraftConfigurationId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftConfiguration]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		AircraftConfigurationId = @AircraftConfigurationId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftLocation]
	@AircraftLocationId Int,  
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @AircraftId INT
	SELECT @AircraftID = AircraftId
	FROM dbo.AircraftLocation
	WHERE AircraftLocationId = @AircraftLocationId
	
	UPDATE [dbo].[AircraftLocation]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		AircraftLocationId = @AircraftLocationId 

	DECLARE @CurrentLocationId INTEGER
	SELECT @CurrentLocationId = ISNULL(LocationId, 0)
	FROM dbo.AircraftLocation
	WHERE AircraftLocationId = 
		(SELECT MAX(AircraftLocationId)
		 FROM AircraftLocation 
		 WHERE AircraftId = @AircraftId 
		 AND LastLocation = 1
		 AND IsActive = 1
		 AND IsDeleted = 0)
	
	UPDATE dbo.Aircraft
	SET 
		CurrentLocationId = @CurrentLocationId,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftStatusDef]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[AircraftStatusDef]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteAircraftTestXref]
	@AircraftId INT,
	@TestId INT,
	@StampUserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftTestXref]
	SET
		IsDeleted = 1,
		UtcDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE	
		AircraftId = @AircraftId AND
		TestId = @TestId
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWeightDef]
	@WeightDefId INT,
	@StampUserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	Update [dbo].[WeightDef]
		SET IsDeleted = 1,
			UtcDateModified = GETUTCDATE(),
			StampUserName = @StampUserName
	WHERE 
		WeightDefId = @WeightDefId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWeightDefType]
	@WeightDefTypeId int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[WeightDefType]
	WHERE 
		WeightDefTypeId = @WeightDefTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWeightUnit]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[WeightUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWOEngineerComment]
	@EngineerCommentsId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOEngineerComment]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		EngineerCommentsId = @EngineerCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWOInstruction]
	@WOInstructionsId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOInstruction]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		WOInstructionsId = @WOInstructionsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWOPerformedWork]
	@PerformedWorkId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOPerformedWork]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		PerformedWorkId = @PerformedWorkId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWOReason]
	@WOReasonId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOReason]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		WOReasonId = @WOReasonId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkOrder]
	@WorkOrderId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrder]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		WorkOrderId = @WorkOrderId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkOrderDocXref]
	@WorkOrderId Int, 
	@DocumentId Int,
	@StampUserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrderDocXref]
	SET
		IsDeleted = 1,
		UtcDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE	
		WorkOrderId = @WorkOrderId AND
		DocumentId = @DocumentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkOrderPart]
	@WorkOrderPartId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrderPart]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		WorkOrderPartId = @WorkOrderPartId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkOrderStatus]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[WorkOrderStatus]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkOrderType]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[WorkOrderType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWorkShift]
	@Id int 
AS
BEGIN
	SET NOCOUNT ON

	DELETE [dbo].[WorkShift]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[deleteWOMechanicComment]
	@WOMechanicCommentsId int, 
	@StampUserName nvarchar(20)  
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOMechanicComment]
	SET IsDeleted = 1,
	    UTCDateModified = GetUTCDate(),
	    StampUserName = @StampUserName
	WHERE 
		WOMechanicCommentsId = @WOMechanicCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fltFetchUserProgramPermissions] 
@UserName AS NVARCHAR(20),
@ProgId AS INT
AS
BEGIN
	SET NOCOUNT ON;
	
	SELECT
		r.IsProgramManager
	FROM
		FltUser AS u, 
		FltUserInRole AS uir, 
		FltRole AS r,
		Program AS p
	WHERE
		u.UserName = uir.UserName AND
		uir.IsDeleted = 0 AND
		uir.RoleId = r.RoleId AND
		r.IsActive = 1 AND
		r.IsDeleted = 0 AND
		r.ProgId = p.ProgID AND
		p.IsActive = 1 AND
		p.IsDeleted = 0 AND
		p.ProgId = @ProgId AND
		u.UserName = @UserName AND
		r.isProgramManager = 1
	GROUP BY
		r.IsProgramManager

	SELECT
		rmp.ModuleName,
		rmp.CanView,
		rmp.CanEdit
	FROM
		FltUser AS u, 
		FltUserInRole AS uir, 
		FltRole AS r, 
		Program AS p,
		fltRoleModulePermissions AS rmp
	WHERE
		u.UserName = uir.UserName AND
		uir.IsDeleted = 0 AND
		uir.RoleId = r.RoleId AND
		r.IsActive = 1 AND
		r.IsDeleted = 0 AND
		r.ProgId = p.ProgID AND
		p.IsActive = 1 AND
		p.IsDeleted = 0 AND
		rmp.RoleId = r.RoleId AND
		rmp.IsDeleted = 0 AND
		p.ProgId = @ProgId AND
		u.UserName = @UserName
	ORDER BY
		rmp.ModuleName
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fltValidateLogin] 
@UserName AS NVARCHAR(20),
@Password AS NVARCHAR(128)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @UTCDate AS DATETIME
	SELECT @UTCDate = GETUTCDATE()
	
	DECLARE @UserCount AS INT
	
	SELECT @UserCount = COUNT(*)
	FROM fltUser
	WHERE	
		LOWER(@username) = LOWER(UserName) AND 
		LOWER(@Password) = LOWER(Password) AND
		FltUser.IsApproved = 1 AND
		FltUser.IsActive = 1 AND
		fltUser.IsDeleted = 0 AND
		fltUser.IsLockedOut = 0
	
	IF @UserCount = 1 
	BEGIN
		
		SELECT
			u.DisplayName,
			ISNULL(p.ProgName,'') AS DefaultProgName,
			ISNULL(u.utcLastLoginDate,'') AS LastLoginDate,
			u.UserName,
			u.DefaultProgId,
			u.IsSuperUser
		FROM
			FltUser AS u 
			LEFT OUTER JOIN Program AS p ON u.DefaultProgId = p.ProgId
		WHERE
			u.UserName = @UserName
		
		UPDATE fltUser
		SET utcLastLoginDate = @UTCDate
		WHERE UserName = @UserName 
		
		DECLARE @IsSuperUser AS BIT
		SELECT @IsSuperUser = 0
		
		SELECT @IsSuperUser = IsSuperUser
		FROM fltUser 
		WHERE UserName = @UserName
		
		IF @IsSuperUser = 1
		BEGIN
			
			SELECT
				ProgId,
				ProgName
			FROM
				Program
			WHERE
				IsActive = 1 AND isDeleted = 0
			ORDER BY
				ProgId
				
		END
		ELSE
		BEGIN
			
			SELECT
				p.ProgId,
				p.ProgName
			FROM
				FltUser AS u, 
				FltUserInRole AS uir, 
				FltRole AS r,
				Program AS p
			WHERE
				u.UserName = uir.UserName AND
				uir.IsDeleted = 0 AND
				uir.RoleId = r.RoleId AND
				r.IsActive = 1 AND
				r.IsDeleted = 0 AND
				r.ProgId = p.ProgID AND
				p.IsActive = 1 AND
				p.IsDeleted = 0 AND
				u.UserName = @UserName
			ORDER BY
				p.ProgID
			
		END
		
	END
			
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAppSetting]
	@AppId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AppSetting]
	WHERE
		[AppId] = @AppId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAPUType]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[APUType]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFltModule]
	@ModuleName 
nvarchar(50),	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FltModule]
	WHERE
		[ModuleName] = @ModuleName

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFltRole]
	@RoleId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FltRole]
	WHERE
		[RoleId] = @RoleId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFltRoleModulePermissions]
	@RoleId 
Int,	@ModuleName 
nvarchar(50),	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FltRoleModulePermissions]
	WHERE
		[RoleId] = @RoleId AND 
		[ModuleName] = @ModuleName

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFltUser]
	@UserName 
nvarchar(20),	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FltUser]
	WHERE
		[UserName] = @UserName

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsDocType]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[DocType]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsDocument]
	@DocId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Document]
	WHERE
		[DocId] = @DocId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsPilot]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Pilot]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsEmission]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Emission]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsEngineer]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Engineer]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsEngineType]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[EngineType]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLengthUnit]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[LengthUnit]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLiquidFill]
	@LiquidFillId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[LiquidFill]
	WHERE
		[LiquidFillId] = @LiquidFillId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLiquidGroup]
	@ID 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[LiquidGroup]
	WHERE
		[ID] = @ID

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLiquidType]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[LiquidType]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLiquidUnit]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[LiquidUnit]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsLocation]
	@LocactionId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Location]
	WHERE
		[LocactionId] = @LocactionId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsManufacturer]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Manufacturer]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsMaturityDef]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[MaturityDef]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsMechAction]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[MechAction]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsModel]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Model]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsProgram]
	@ProgID 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Program]
	WHERE
		[ProgID] = @ProgID

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsRoute]
	@RouteId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Route]
	WHERE
		[RouteId] = @RouteId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsMechanic]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Mechanic]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsRegion]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Region]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsTest]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Test]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsPropeller]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Propeller]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsPropellerType]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[PropellerType]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFlight]
	@FlightId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Flight]
	WHERE
		[FlightId] = @FlightId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFlightComment]
	@FlightCommentId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FlightComment]
	WHERE
		[FlightCommentId] = @FlightCommentId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFlightEngineRun]
	@FlightEngineRunId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FlightEngineRun]
	WHERE
		[FlightEngineRunId] = @FlightEngineRunId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsFlightRegionTime]
	@FlightRegionTimeId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[FlightRegionTime]
	WHERE
		[FlightRegionTimeId] = @FlightRegionTimeId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsUTCTimeZone]
	@TimeZoneId int,
	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[UTCTimeZone]
	WHERE
		[TimeZoneId] = @TimeZoneId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraft]
	@AircraftId 
Int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[Aircraft]
	WHERE
		[AircraftId] = @AircraftId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftComponent]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AircraftComponent]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftConfiguration]
	@AircraftConfigurationId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AircraftConfiguration]
	WHERE
		[AircraftConfigurationId] = @AircraftConfigurationId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftLocation]
	@AircraftLocationId 
Int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AircraftLocation]
	WHERE
		[AircraftLocationId] = @AircraftLocationId 

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftStatusDef]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AircraftStatusDef]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftTest]
	@AircraftTestId int,	
	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[AircraftTest]
	WHERE
		[AircraftTestId] = @AircraftTestId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsAircraftTestXref]
	@AircraftId INT,
	@TestId INT,
	@Exists BIT OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @Count INTEGER
	
	SELECT 
		@Count = COUNT(*)
	FROM
		[dbo].[AircraftTestXref]
	WHERE	
		AircraftId = @AircraftId AND
		TestId = @TestId AND
		IsDeleted = 0
		
	IF @Count > 0
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWOEngineerComment]
	@EngineerCommentsId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WOEngineerComment]
	WHERE
		[EngineerCommentsId] = @EngineerCommentsId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWOInstruction]
	@WOInstructionsId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WOInstruction]
	WHERE
		[WOInstructionsId] = @WOInstructionsId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWOPerformedWork]
	@PerformedWorkId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WOPerformedWork]
	WHERE
		[PerformedWorkId] = @PerformedWorkId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWorkOrder]
	@WorkOrderId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WorkOrder]
	WHERE
		[WorkOrderId] = @WorkOrderId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWorkOrderPart]
	@WorkOrderPartId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WorkOrderPart]
	WHERE
		[WorkOrderPartId] = @WorkOrderPartId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWorkShift]
	@Id 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WorkShift]
	WHERE
		[Id] = @Id

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[existsWOMechanicComment]
	@WOMechanicCommentsId 
int,	@Exists bit OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @ExistsCount INT
	
	SELECT @ExistsCount = Count(*) FROM [dbo].[WOMechanicComment]
	WHERE
		[WOMechanicCommentsId] = @WOMechanicCommentsId

	IF (@ExistsCount <> 0)
		SELECT @Exists = 1
	ELSE
		SELECT @Exists = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAppSetting]
	@AppId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[AppSetting]
	WHERE
		[AppId] = @AppId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAPUType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[APUType]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFltModule]
	@ModuleName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[FltModule]
	WHERE
		[ModuleName] = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFltRole]
	@RoleId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[FltRole].*,
		Program.ProgName
	FROM 
		[dbo].[FltRole]
		INNER JOIN Program ON Program.ProgId = FltRole.ProgId
	WHERE
		[RoleId] = @RoleId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFltRoleModulePermissions]
	@RoleId Int,
	@ModuleName nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[FltRoleModulePermissions]
	WHERE
		[RoleId] = @RoleId AND 
		[ModuleName] = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFltUser]
	@UserName nvarchar(20)
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[FltUser]
	WHERE
		[UserName] = @UserName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchDocType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[DocType]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchDocument]
	@DocumentId AS INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		Document.DocumentId,
		DocumentInfo.DocumentSize,
		Document.DocumentContent
	FROM 
		dbo.Document 
		INNER JOIN dbo.DocumentInfo ON
			dbo.DocumentInfo.DocumentId = dbo.Document.DocumentId
	WHERE 
		dbo.Document.DocumentId = @DocumentId
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchDocumentInfo]
	@DocumentId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		DocumentInfo.DocumentId,
		DocumentInfo.ProgId,
		DocumentInfo.DocumentTypeId,
		DocumentType.FileExtension,
		DocumentType.MimeType,
		DocumentInfo.DocumentName,
		DocumentInfo.DocumentDescription,
		DocumentInfo.DocumentSize,
		DocumentInfo.IsDeleted,
		DocumentInfo.UTCDateCreated,
		DocumentInfo.UTCDateModified,
		DocumentInfo.StampUserName,
		DocumentInfo.ConcurrencyId
	FROM [dbo].[DocumentInfo]
		INNER JOIN DocumentType ON
			DocumentType.DocumentTypeId = DocumentInfo.DocumentTypeId
	WHERE
		[DocumentId] = @DocumentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchDocumentType]
	@DocumentTypeId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[DocumentType]
	WHERE
		[DocumentTypeId] = @DocumentTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchPilot]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Pilot]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchEmission]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Emission]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchEngineer]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Engineer]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchEngineeringAction]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[EngineeringAction]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchEngineType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[EngineType]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchIssueCode]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[IssueCode]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLengthUnit]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[LengthUnit]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLiquidFill]
	@LiquidFillId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].[LiquidFill].*,
		[dbo].[LiquidGroup].[StringValue] AS LiquidGroupName,
		[dbo].[LiquidType].[StringValue] AS LiquidTypeName,
		[dbo].[LiquidUnit].[StringValue] AS LiquidUnitName
	FROM 
		[dbo].[LiquidFill]
		INNER JOIN [dbo].[LiquidGroup] ON
			[dbo].[LiquidGroup].[Id] = [dbo].[LiquidFill].[LiquidGroupId]
		INNER JOIN [dbo].[LiquidType] ON
			[dbo].[LiquidType].[Id] = [dbo].[LiquidFill].[LiquidTypeId]
		INNER JOIN [dbo].[LiquidUnit] ON
			[dbo].[LiquidUnit].[Id] = [dbo].[LiquidFill].[LiquidUnitId]
	WHERE
		[LiquidFillId] = @LiquidFillId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLiquidGroup]
	@ID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[LiquidGroup]
	WHERE
		[ID] = @ID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLiquidType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[LiquidType]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLiquidUnit]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[LiquidUnit]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLocation]
	@LocactionId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[Location].*,
		UTCTimeZone.TimeZoneCode,
		UTCTimeZone.TimeZoneOffset,
		UTCTimeZone.DaylightSavingTime,
		UTCTimeZone.DaylightSavingOffset,
		UTCTimeZone.DaylightSavingBegin,
		UTCTimeZone.DaylightSavingEnd
	FROM 
		[dbo].[Location]
		INNER JOIN [dbo].[UTCTimeZone] ON
			[dbo].[UTCTimeZone].TimeZoneId = [dbo].[Location].TimeZoneId
	WHERE
		[LocactionId] = @LocactionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchManufacturer]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Manufacturer]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchMaturityDef]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[MaturityDef]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchMechAction]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[MechAction]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchModel]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Model]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchProgram]
	@ProgID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Program]
	WHERE
		[ProgID] = @ProgID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchRoute]
	@RouteId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].[Route].*,
		[dbo].[UTCTimeZone].TimeZoneCode
	FROM 
		[dbo].[Route]
		INNER JOIN [dbo].[UTCTimeZone] ON
			[dbo].[UTCTimeZone].TimeZoneId = [dbo].[Route].TimeZoneId		
	WHERE
		[RouteId] = @RouteId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchMechanic]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Mechanic]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchRegion]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Region]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchTest]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Test]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchLoadingConfig]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[LoadingConfig]
	WHERE
		[Id] = @Id

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchPropeller]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[Propeller]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchPropellerType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[PropellerType]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[fetchFlight]
@FlightId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT [Flight].*,
           SchedRoute.[Name] AS SchedRouteName,
           FlownRoute.[Name] AS FlownRouteName,
           Workshift.StringValue AS WorkShiftName,
           ISNULL(Pilot.StringValue, '<not assigned>') AS PilotName,
           ISNULL(AirframeWeightDef.Weight, 0) + ISNULL(Cargo1WeightDef.[Weight], 0) + ISNULL(Cargo2WeightDef.[Weight], 0) + ISNULL(BallastWeightDef.[Weight], 0) AS TakeoffWeight,
           ISNULL(AirframeWeightDef.WeightDefDescr, '') AS AirframeWeightDescr,
           ISNULL(AirframeWeightDef.[Weight], 0) AS AirframeWeight,
           ISNULL(AirframeWeightUnit.StringValue, '') AS AirframeWeightUnit,
           AirframeWeightDef.WeightDefGUID AS AirframeWeightGUID,
           ISNULL(Cargo1WeightDef.WeightDefDescr, '') AS Cargo1WeightDescr,
           ISNULL(Cargo1WeightDef.[Weight], 0) AS Cargo1Weight,
           ISNULL(Cargo1WeightUnit.StringValue, '') AS Cargo1WeightUnit,
           Cargo1WeightDef.WeightDefGUID AS Cargo1WeightGUID,
           ISNULL(Cargo2WeightDef.WeightDefDescr, '') AS Cargo2WeightDescr,
           ISNULL(Cargo2WeightDef.[Weight], 0) AS Cargo2Weight,
           ISNULL(Cargo2WeightUnit.StringValue, '') AS Cargo2WeightUnit,
           Cargo2WeightDef.WeightDefGUID AS Cargo2WeightGUID,
           ISNULL(BallastWeightDef.WeightDefDescr, '') AS BallastWeightDescr,
           ISNULL(BallastWeightDef.[Weight], 0) AS BallastWeight,
           ISNULL(BallastWeightUnit.StringValue, '') AS BallastWeightUnit,
           BallastWeightDef.WeightDefGUID AS BallastWeightGUID
    FROM   [dbo].[Flight]
           INNER JOIN
           [dbo].[Route] AS SchedRoute
           ON SchedRoute.RouteId = Flight.SchedRouteId
           INNER JOIN
           [dbo].[Route] AS FlownRoute
           ON FlownRoute.RouteId = Flight.FlownRouteId
           INNER JOIN
           dbo.WorkShift
           ON Workshift.Id = Flight.WorkShiftIndicatorId
           LEFT OUTER JOIN
           dbo.Pilot
           ON Pilot.Id = Flight.PilotId
           LEFT OUTER JOIN
           WeightDef AS AirframeWeightDef
           ON AirframeWeightDef.WeightDefId = Flight.AirframeWeightId
           LEFT OUTER JOIN
           WeightUnit AS AirframeWeightUnit
           ON AirframeWeightUnit.Id = AirframeWeightDef.WeightUnitId
           LEFT OUTER JOIN
           WeightDef AS Cargo1WeightDef
           ON Cargo1WeightDef.WeightDefId = Flight.Cargo1WeightId
           LEFT OUTER JOIN
           WeightUnit AS Cargo1WeightUnit
           ON Cargo1WeightUnit.Id = Cargo1WeightDef.WeightUnitId
           LEFT OUTER JOIN
           WeightDef AS Cargo2WeightDef
           ON Cargo2WeightDef.WeightDefId = Flight.Cargo2WeightId
           LEFT OUTER JOIN
           WeightUnit AS Cargo2WeightUnit
           ON Cargo2WeightUnit.Id = Cargo2WeightDef.WeightUnitId
           LEFT OUTER JOIN
           WeightDef AS BallastWeightDef
           ON BallastWeightDef.WeightDefId = Flight.BallastWeightId
           LEFT OUTER JOIN
           WeightUnit AS BallastWeightUnit
           ON BallastWeightUnit.Id = BallastWeightDef.WeightUnitId
    WHERE  [FlightId] = @FlightId;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFlightComment]
	@FlightCommentId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].[FlightComment].*,
		[dbo].[AircraftComponent].StringValue AS ComponentName
	FROM 
		[dbo].[FlightComment]
		INNER JOIN [dbo].[AircraftComponent] ON
			[dbo].[AircraftComponent].Id = [dbo].[FlightComment].ComponentId			
	WHERE
		[FlightCommentId] = @FlightCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFlightKeyInfo]
	@AircraftId INT,
	@LocationId INT,
	@FlightDateSort VARCHAR(10),
	@WorkShiftId INT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @MSN VARCHAR(20)
	DECLARE @RegistrationNbr VARCHAR(10)
	DECLARE @SerialNbr VARCHAR(6)
	DECLARE @AirframeHoursCurrent DECIMAL(9,1)
	DECLARE @AirframeHoursOffset DECIMAL(9,1)
	DECLARE @MeterUnitId INT
	DECLARE @ValidAircraft BIT
	
	SELECT
		@Msn = MSN,
		@RegistrationNbr = RegistrationNbr,
		@SerialNbr = SerialNbr,
		@AirframeHoursCurrent = AirframeHoursCurrent,
		@MeterUnitId = MeterUnitId,
		@ValidAircraft = CONVERT(BIT, ISNULL(AircraftId, 0))
	FROM
		Aircraft
	WHERE
		AircraftId = @AircraftId
		AND IsActive = 1 AND IsDeleted = 0
	
	DECLARE @FlightDate DATETIME
	SELECT @FlightDate = CONVERT(DATETIME, @FlightDateSort)
	
	SELECT 
		@AirframeHoursOffset = A.ConfigurationMeterOffset
	FROM
		(SELECT TOP(1)*
		 FROM 
			dbo.AircraftConfiguration
		 WHERE
			AircraftId = @AircraftId 
			AND	ConfigurationStartDate <= @FlightDate
		 ORDER BY
			ConfigurationStartDate DESC) AS A
		WHERE
			@FlightDate >= A.ConfigurationStartDate AND
			(A.ConfigurationEndDate IS NULL OR @FlightDate < A.ConfigurationEndDate)
	
	DECLARE @MeterUnit NVARCHAR(255)
	
	SELECT @MeterUnit = StringValue 
	FROM dbo.LengthUnit
	WHERE dbo.LengthUnit.id = @MeterUnitId
	
	DECLARE @LocationName NVARCHAR(50)
	DECLARE @ValidLocation BIT
	
	SELECT 
		@LocationName = LocationName,
		@ValidLocation = CONVERT(BIT, ISNULL(LocactionId, 0))
	FROM
		dbo.Location
	WHERE
		LocactionId = @LocationId AND
		IsActive = 1 AND IsDeleted = 0
	
	DECLARE @ValidAircraftLocation BIT
	
	SELECT
		@ValidAircraftLocation = CONVERT(BIT, ISNULL(AircraftLocationId, 0))
	FROM
		dbo.AircraftLocation
	WHERE
		AircraftId = @AircraftID AND
		LocationId = @LocationId AND
		IsActive = 1 AND IsDeleted = 0	
		
	DECLARE @WorkShift NVARCHAR(255)
	DECLARE @ValidWorkShift BIT
	
	SELECT
		@WorkShift = StringValue,
		@ValidWorkShift = CONVERT(BIT, ISNULL(Id, 0))
	FROM
		WorkShift
	WHERE
		Id = @WorkShiftId
	
	
	DECLARE @FlightId INT
	DECLARE @ValidFlight BIT
	
	SELECT
		@FlightId = Flight.FlightId,
		@ValidFlight = CONVERT(BIT, ISNULL(FlightId, 0))
	FROM
		Flight
	WHERE
		Flight.AircraftId = @AircraftId 
		AND Flight.LocationId = @LocationId 
		AND Flight.FlightDateSort = @FlightDateSort
		AND Flight.WorkShiftIndicatorId = @WorkShiftId
		AND IsActive = 1 AND IsDeleted = 0
	
	SELECT
		@Flightid AS FlightId,
		@AircraftId AS AircraftId,
		@LocationId AS LocationId,
		@FlightDateSort AS FlightDateSort,
		@WorkShiftId AS WorkShiftId,
		@MSN AS MSN,
		@RegistrationNbr AS RegistrationNbr,
		@SerialNbr AS SerialNbr,
		@AirframeHoursCurrent AS AirframeHoursCurrent,
		ISNULL(@AirframeHoursOffset, 0) AS AirframeHoursOffset,
		@MeterUnitId AS MeterUnitId,
		@MeterUnit AS MeterUnit,
		@LocationName AS LocationName,
		@WorkShift AS WorkShift,
		@ValidAircraft AS ValidAircraft,
		@ValidLocation AS ValidLocation,
		@ValidAircraftLocation AS ValidAircraftLocation,
		@ValidWorkShift AS ValidWorkShift,
		@ValidFlight AS ValidFlight

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFlightEngineRun]
	@FlightEngineRunId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].[FlightEngineRun].* 
	FROM 
		[dbo].[FlightEngineRun]
	WHERE
		[FlightEngineRunId] = @FlightEngineRunId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchFlightRegionTime]
	@FlightRegionTimeId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].[FlightRegionTime].* ,
		[dbo].[Region].[Code] AS RegionCode
	FROM 
		[dbo].[FlightRegionTime]
		INNER JOIN [dbo].[Region] ON 
			[dbo].[Region].[ID] = [dbo].[FlightRegionTime].RegionId
	WHERE
		[FlightRegionTimeId] = @FlightRegionTimeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchUTCTimeZone]
	@TimeZoneId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[UTCTimeZone]
	WHERE
		[TimeZoneId] = @TimeZoneId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraft]
	@AircraftId Int
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #tempFlightUsage (
		#tempFlightUsageId INT IDENTITY PRIMARY KEY,
		AircraftId INT,
		CurrentFlightUsage DECIMAL(8,1)
	)
	
	INSERT INTO #tempFlightUsage (
		AircraftId,
		CurrentFlightUsage
		)
	SELECT
		AircraftId,
		SUM(FlightUsage)
	FROM
		(SELECT
			AircraftId,
			FlightUsage = (HobbsEnd - HobbsStart)
		FROM
			dbo.Flight
		WHERE
			dbo.Flight.AircraftId = @AircraftId AND
			dbo.flight.IsActive = 1 AND
			dbo.flight.IsDeleted = 0) AS T
	GROUP BY T.AircraftId
	
	SELECT 
		[dbo].[Aircraft].AircraftId,
		[dbo].[Aircraft].ProgId,
		[dbo].[Aircraft].CurrentLocationId,
		[dbo].[Aircraft].MSN,
		[dbo].[Aircraft].ShipNbr,
		[dbo].[Aircraft].SerialNbr,
		[dbo].[Aircraft].ModelId,
		[dbo].[Aircraft].Description,
		[dbo].[Aircraft].EngineerId,
		[dbo].[Aircraft].MechanicId,
		[dbo].[Aircraft].RegistrationNbr,
		[dbo].[Aircraft].LiveryColor,
		[dbo].[Aircraft].AircraftStatusId,
		[dbo].[Aircraft].ManufacturerId,
		[dbo].[Aircraft].MeterUnitId,
		[dbo].[Aircraft].AirframeHoursStart,
		[dbo].[Aircraft].TotalRequiredUsage,
		[dbo].[Aircraft].EstEndOfTesting,
		[dbo].[Aircraft].ActEndOfTesting,
		[dbo].[Aircraft].AircraftGUID,
		[dbo].[Aircraft].IsActive,
		[dbo].[Aircraft].IsDeleted,
		[dbo].[Aircraft].UTCDateCreated,
		[dbo].[Aircraft].UTCDateModified,
		[dbo].[Aircraft].StampUserName,
		[dbo].[Aircraft].ConcurrencyId,
		[dbo].[Aircraft].AirframeHoursCurrent,
		[dbo].[Aircraft].AirframeHoursOffset,
		ISNULL(#tempFlightUsage.CurrentFlightUsage, 0) AS [CurrentFlightUsage],
		ISNULL([dbo].[Location].LocationName, 'Not Assigned') AS LocationName,
		[dbo].[LengthUnit].[StringValue] AS MeterUnit,
		[dbo].[Aircraft].[AircraftEngineHoursCurrent],
		[dbo].[Model].[StringValue] AS [Model],
		[dbo].[Engineer].[StringValue] AS [Engineer],
		[dbo].[Mechanic].[StringValue] AS [Mechanic],
		[dbo].[AircraftStatusDef].[StringValue] AS [AircraftStatus],
		[dbo].[Manufacturer].[StringValue] AS [Manufacturer]
	FROM 
		[dbo].[Aircraft]
		LEFT OUTER JOIN [dbo].Location ON
			[dbo].[Location].[LocactionId] = [dbo].[Aircraft].[CurrentLocationId]
		INNER JOIN [dbo].[Model] ON
			[dbo].[Model].[Id] = [dbo].[Aircraft].[ModelId]
		INNER JOIN [dbo].[Engineer] ON
			[dbo].[Engineer].[Id] = [dbo].[Aircraft].[EngineerId]
		INNER JOIN [dbo].[Mechanic] ON
			[dbo].[Mechanic].[Id] = [dbo].[Aircraft].[MechanicId]
		INNER JOIN [dbo].[AircraftStatusDef] ON
			[dbo].[AircraftStatusDef].[Id] = [dbo].[Aircraft].[AircraftStatusId]
		INNER JOIN [dbo].[Manufacturer] ON
			[dbo].[Manufacturer].[Id] = [dbo].[Aircraft].[ManufacturerId]
		INNER JOIN [dbo].[LengthUnit] ON
			[dbo].[LengthUnit].[Id] = [dbo].[Aircraft].[MeterUnitId]
		LEFT OUTER JOIN #tempFlightUsage ON
			#tempFlightUsage.AircraftId = @AircraftId
	WHERE
		[Aircraft].[AircraftId] = @AircraftId

	DROP TABLE #tempFlightUsage
	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftComment]
	@AircraftCommentId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[AircraftComment]
	WHERE
		[AircraftCommentId] = @AircraftCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftComponent]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[AircraftComponent]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftConfiguration]
	@AircraftConfigurationId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[AircraftConfiguration].*,
		[dbo].[LengthUnit].[StringValue] AS [MeterUnit],
		[dbo].[Model].[StringValue] AS [ModelName],
		[dbo].[EngineType].[StringValue] AS [EngineType],
		[EngineEmissionDef].[StringValue] AS [EngineEmission],
		[EngineMaturityDef].[StringValue] AS [EngineMaturity],
		[dbo].[APUType].[StringValue] AS [APUType],
		[APUEmissionDef].StringValue AS [APUEmission],
		[APUMaturityDef].StringValue AS [APUMaturity],
		[dbo].[PropellerType].[StringValue] AS [PropellerType]	
	FROM 
		[dbo].[AircraftConfiguration]
		INNER JOIN [dbo].[EngineType] ON
			[dbo].[EngineType].[Id] = [dbo].[AircraftConfiguration].[EngineTypeId]
		INNER JOIN [dbo].[Emission] AS [EngineEmissionDef] ON
			[EngineEmissionDef].[Id] = [dbo].[AircraftConfiguration].[EngineEmissionId]
		INNER JOIN [dbo].[MaturityDef] AS [EngineMaturityDef] ON
			[EngineMaturityDef].[Id] = [dbo].[AircraftConfiguration].[EngineMaturityId]
		INNER JOIN [dbo].[APUType] ON
			[dbo].[APUType].[Id] = [dbo].[AircraftConfiguration].[APUTypeId]
		INNER JOIN [dbo].[Emission] AS [APUEmissionDef] ON
			[APUEmissionDef].[Id] = [dbo].[AircraftConfiguration].[APUEmissionId]
		INNER JOIN [dbo].[MaturityDef] AS [APUMaturityDef] ON
			[APUMaturityDef].[Id] = [dbo].[AircraftConfiguration].[APUMaturityId]
		INNER JOIN [dbo].[PropellerType] ON
			[dbo].[PropellerType].[Id] = [dbo].[AircraftConfiguration].[PropellerTypeId]
		INNER JOIN [LengthUnit] ON
			[dbo].[LengthUnit].[Id] = [dbo].[AircraftConfiguration].[MeterUnitId]
		INNER JOIN [dbo].[Model] ON
			[dbo].[Model].[ID] = [dbo].[AircraftConfiguration].[ModelId]
	WHERE
		[AircraftConfigurationId] = @AircraftConfigurationId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[fetchAircraftLocation]
	@AircraftLocationId Int
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @AircraftId AS INT
	DECLARE @LocationId AS INT
	DECLARE @ArrivalDate AS DATETIME
	DECLARE @ActEndOfTest AS DATETIME
	
	SELECT 
		@AircraftId = AircraftId,
		@LocationId = LocationId,
		@ArrivalDate = ArrivalDate,
		@ActendOfTest = ISNULL(ActEndOfTest, Convert(DATETIME, '12-31-2100'))
	FROM
		dbo.AircraftLocation
	WHERE
		AircraftLocationId = @AircraftLocationId
		
	CREATE TABLE #tempFlightUsage (
		#tempFlightUsageId INT IDENTITY PRIMARY KEY,
		AircraftId INT,
		LocationId INT,
		CurrentUsage DECIMAL(8,1)
	)
	
	INSERT INTO #tempFlightUsage (
		AircraftId,
		LocationId,
		CurrentUsage
		)
	SELECT
		AircraftId,
		LocationId,
		SUM(FlightUsage)
	FROM
		(SELECT
			AircraftId,
			LocationId,
			FlightUsage = (MeterEnd - MeterStart)
		FROM
			dbo.Flight
		WHERE
		    dbo.Flight.LocationId = @LocationId AND
			dbo.Flight.AircraftId = @AircraftId AND
			dbo.flight.DepartureDateTime_Local >= @ArrivalDate AND
			dbo.flight.DepartureDateTime_Local <= @ActEndOfTest AND
			dbo.flight.IsActive = 1 AND
			dbo.flight.IsDeleted = 0) AS T
	GROUP BY 
		T.AircraftId,
		T.LocationId
	
	SELECT 
		[dbo].[AircraftLocation].AircraftLocationid,
		[dbo].aircraftLocation.AircraftId,
		dbo.AircraftLocation.LocationId,
		dbo.AircraftLocation.ArrivalDate,
		dbo.AircraftLocation.RequiredUsage,
		ISNULL(#tempFlightUsage.CurrentUsage, 0) AS AccumUsage,
		dbo.AircraftLocation.EstEndOfTest,
		dbo.AircraftLocation.ActEndOfTest,
		dbo.AircraftLocation.LastLocation,
		dbo.AircraftLocation.AircraftLocationGUID,
		dbo.AircraftLocation.IsActive,
		dbo.AircraftLocation.IsDeleted,
		dbo.AircraftLocation.UTCDateCreated,
		dbo.AircraftLocation.UTCDateModified,
		dbo.AircraftLocation.StampUserName,
		dbo.AircraftLocation.ConcurrencyId,
		[dbo].Aircraft.SerialNbr,
		[dbo].Aircraft.RegistrationNbr,
		[dbo].Aircraft.MSN,
		[dbo].Aircraft.ShipNbr,
		[dbo].Location.LocationName
	FROM 
		[dbo].[AircraftLocation]
		LEFT OUTER JOIN #tempFlightUsage ON 
			dbo.aircraftLocation.aircraftId = #tempFlightUsage.AircraftId AND dbo.AircraftLocation.LocationId = #tempFlightUsage.LocationId
		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[AircraftLocation].[AircraftID]
		INNER JOIN [dbo].[Location] ON
			[dbo].[Location].[LocactionId] = [dbo].[AircraftLocation].[LocationId]
	WHERE
		[AircraftLocationId] = @AircraftLocationId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftStatus]
	@AircraftId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		v.AircraftId,
		IsNull(vs.AircraftStatusId, 0) as AircraftStatusId,
		IsNull(loc.LocationName, '') as LocationName, 
		v.MSN, 
		IsNull(vs.Status, '') as Status,
		IsNull(vs.EngineerComment, '') as EngineerComment, 
		IsNull(vs.HangarComment, '') as HangarComment, 
		vs.UTCDateModified 
	FROM 
		Aircraft v
		left outer join AircraftStatus vs 
			ON v.AircraftId = vs.AircraftId
		left outer join Location loc
			ON v.CurrentLocationId = loc.LocactionId
	WHERE 
		v.IsDeleted = 0 AND 
		v.AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchAircraftStatusDef]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[AircraftStatusDef]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWeightDef]
	@WeightDefId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		dbo.WeightDef.*,
		WeightDefType.WeightDefTypeDescr,
		WeightUnit.StringValue AS WeightUnit
	FROM [dbo].[WeightDef]
	INNER JOIN WeightDefType ON dbo.WeightDefType.WeightDefTypeId = WeightDef.WeightDefTypeId
	INNER JOIN WeightUnit ON WeightUnit.Id = WeightDef.WeightUnitId
	WHERE
		[WeightDefId] = @WeightDefId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWeightDefType]
	@WeightDefTypeId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WeightDefType]
	WHERE
		[WeightDefTypeId] = @WeightDefTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWeightUnit]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WeightUnit]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWOEngineerComment]
	@EngineerCommentsId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WOEngineerComment]
	WHERE
		[EngineerCommentsId] = @EngineerCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWOInstruction]
	@WOInstructionsId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WOInstruction]
	WHERE
		[WOInstructionsId] = @WOInstructionsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWOPerformedWork]
	@PerformedWorkId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WOPerformedWork]
	WHERE
		[PerformedWorkId] = @PerformedWorkId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWOReason]
	@WOReasonId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WOReason]
	WHERE
		[WOReasonId] = @WOReasonId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWorkOrder]
	@WorkOrderId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		WorkOrder.WorkOrderId,
		WorkOrder.WorkOrderNbr,
		WorkOrder.ProgId,
		WorkOrder.AircraftId,
		Aircraft.MSN,
		Aircraft.ShipNbr,
		Aircraft.SerialNbr,
		Aircraft.RegistrationNbr,
		WorkOrder.LocationId,
		Location.LocationName,
		WorkOrder.WorkOrderTypeId,
		WorkOrderType.StringValue AS WorkOrderType,
		WorkOrder.WorkOrderStatusId,
		WorkOrderStatus.StringValue AS WorkOrderStatus,
		WorkOrder.EngineerId,
		Engineer.StringValue AS Engineer,
		WorkOrder.EngineeringActionId,
		EngineeringAction.StringValue AS EngineeringAction,
		WorkOrder.MechanicId,
		ISNULL(Mechanic.StringValue, '') AS Mechanic,
		WorkOrder.RequestDate,
		WorkOrder.FaultOccurredDate,
		WorkOrder.FaultOccurredMeter,
		WorkOrder.FISComplaintNbr,
		WorkOrder.SquawkNbr,
		WorkOrder.DueDate,
		WorkOrder.DueTime,
		WorkOrder.MeterStartWork,
		WorkOrder.FinishDate,
		WorkOrder.FinishTime,
		WorkOrder.MeterEndWork,
		WorkOrder.AircraftDispostion,
		WorkOrder.AircraftIssueAlert,
		WorkOrder.MaintenanceIssueAlert,
		WorkOrder.IsDeleted,
		WorkOrder.WorkOrderGuid,
		WorkOrder.UTCDateCreated,
		WorkOrder.UTCDateModified,
		WorkOrder.StampUserName,
		WorkOrder.ConcurrencyId
	FROM 
		[dbo].[WorkOrder]
		INNER JOIN dbo.Aircraft ON
			Aircraft.AircraftId = WorkOrder.AircraftId
		INNER JOIN dbo.Location ON
			dbo.Location.LocactionId = WorkOrder.LocationId
		INNER JOIN WorkOrderStatus ON
			dbo.WorkOrderStatus.ID = WorkOrder.WorkOrderStatusId
		INNER JOIN WorkOrderType ON
			dbo.WorkOrderType.ID = WorkOrder.WorkOrderTypeId
		INNER JOIN dbo.Engineer ON
			Engineer.Id = WorkOrder.EngineerId
		INNER JOIN dbo.EngineeringAction ON
			EngineeringAction.ID = WorkOrder.EngineeringActionId
		LEFT OUTER JOIN dbo.Mechanic ON
			dbo.Mechanic.ID = WorkOrder.MechanicId
	WHERE
		[WorkOrderId] = @WorkOrderId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWorkOrderPart]
	@WorkOrderPartId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WorkOrderPart]
	WHERE
		[WorkOrderPartId] = @WorkOrderPartId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWorkOrderStatus]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WorkOrderStatus]
	WHERE
		[Id] = @Id
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWorkOrderType]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WorkOrderType]
	WHERE
		[Id] = @Id
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWorkShift]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WorkShift]
	WHERE
		[Id] = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[fetchWOMechanicComment]
	@WOMechanicCommentsId int
AS
BEGIN
	SET NOCOUNT ON

	SELECT * FROM [dbo].[WOMechanicComment]
	WHERE
		[WOMechanicCommentsId] = @WOMechanicCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAppSettings]
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[AppId] [Int]
	)

	INSERT INTO #temp (
		[AppId]
	)
	SELECT
		[AppId]
	FROM [dbo].[AppSetting]
	WHERE
		((@IsActiveFilter = 1 AND [AppSetting].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AppSetting].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [AppSetting].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [AppSetting].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[AppSetting].* FROM [dbo].[AppSetting]
	INNER JOIN #temp ON 
		[dbo].[AppSetting].[AppId] = #temp.[AppId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[AppSetting]
	WHERE
		((@IsActiveFilter = 1 AND [AppSetting].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AppSetting].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [AppSetting].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [AppSetting].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAPUTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[APUType]
	WHERE
		((@IsActiveFilter = 1 AND [APUType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [APUType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[APUType].[ProgId] = @ProgId))
	ORDER BY
		[APUType].[SortOrder],
		[APUType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[APUType].* FROM [dbo].[APUType]
	INNER JOIN #temp ON 
		[dbo].[APUType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[APUType]
	WHERE
		((@IsActiveFilter = 1 AND [APUType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [APUType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[APUType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltModules]
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[ModuleName] [NVarChar](50)
	)

	INSERT INTO #temp (
		[ModuleName]
	)
	SELECT
		[ModuleName]
	FROM [dbo].[FltModule]
	WHERE
		((@IsActiveFilter = 1 AND [FltModule].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltModule].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltModule].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltModule].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[FltModule].* FROM [dbo].[FltModule]
	INNER JOIN #temp ON 
		[dbo].[FltModule].[ModuleName] = #temp.[ModuleName]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[FltModule]
	WHERE
		((@IsActiveFilter = 1 AND [FltModule].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltModule].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltModule].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltModule].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltRoleModulePermissionses]
	@RoleId Int = 0,
	@ModuleName nvarchar(50) = '',
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[RoleId] [Int],
		[ModuleName] [NVarChar](50)
	)

	INSERT INTO #temp (
		[RoleId],
		[ModuleName]
	)
	SELECT
		[RoleId],
		[ModuleName]
	FROM [dbo].[FltRoleModulePermissions]
	WHERE
		((@IsActiveFilter = 1 AND [FltRoleModulePermissions].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltRoleModulePermissions].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltRoleModulePermissions].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltRoleModulePermissions].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@RoleId = 0) OR ([dbo].[FltRoleModulePermissions].[RoleId] = @RoleId)) AND 
		((@ModuleName = '') OR ([dbo].[FltRoleModulePermissions].[ModuleName] = @ModuleName))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[FltRoleModulePermissions].* FROM [dbo].[FltRoleModulePermissions]
	INNER JOIN #temp ON 
		[dbo].[FltRoleModulePermissions].[RoleId] = #temp.[RoleId] AND 
		[dbo].[FltRoleModulePermissions].[ModuleName] = #temp.[ModuleName]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[FltRoleModulePermissions]
	WHERE
		((@IsActiveFilter = 1 AND [FltRoleModulePermissions].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltRoleModulePermissions].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltRoleModulePermissions].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltRoleModulePermissions].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@RoleId = 0) OR ([dbo].[FltRoleModulePermissions].[RoleId] = @RoleId)) AND 
		((@ModuleName = '') OR ([dbo].[FltRoleModulePermissions].[ModuleName] = @ModuleName))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltRoles]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[RoleId] [Int]
	)

	INSERT INTO #temp (
		[RoleId]
	)
	SELECT
		[RoleId]
	FROM 
		[dbo].[FltRole]
		INNER JOIN Program ON Program.ProgId = FltRole.ProgId
	WHERE
		((@IsActiveFilter = 1 AND [FltRole].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltRole].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltRole].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltRole].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[FltRole].[ProgId] = @ProgId))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[FltRole].*,
		Program.ProgName
	FROM 
		[dbo].[FltRole]
		INNER JOIN Program ON 
			Program.ProgId = FltRole.ProgId
		INNER JOIN #temp ON 
			[dbo].[FltRole].[RoleId] = #temp.[RoleId]
	WHERE 
		#temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY 
		#temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltRolesForFltUser]
	@UserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[FltRole].*,
		Program.ProgName 
	FROM 
		FltRole 
		INNER JOIN dbo.Program
			ON dbo.Program.ProgId = FltRole.ProgId
			AND dbo.Program.IsActive = 1 AND dbo.Program.IsDeleted = 0
		INNER JOIN fltUserInRole 
			ON fltUserInRole.RoleId = FltRole.RoleID
		INNER JOIN fltUser 
			ON fltUser.UserName = FltUserInRole.UserName
	WHERE 
		FltUser.UserName = @UserName
		AND FltUserInRole.IsDeleted = 0
		AND FltRole.IsActive = 1
		AND FltRole.IsDeleted = 0
		AND FltUser.IsActive = 1
		AND FltUser.IsDeleted = 0
	ORDER BY 
		Program.ProgName,
		fltRole.RoleName
	
END

SET ANSI_NULLS ON

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltRolesNotForFltUser]
	@UserName NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON
	
	SELECT 
		[FltRole].*,
		Program.ProgName
	FROM
		[FltRole]
		INNER JOIN Program ON Program.ProgId = FltRole.ProgId
			AND Program.IsActive = 1 and Program.IsDeleted = 0
	WHERE 
		[FltRole].IsActive = 1
		AND [FltRole].IsDeleted = 0
		AND [FltRole].RoleId NOT IN (
		SELECT 
			[FltRole].RoleId 
		FROM FltRole 
			INNER JOIN fltUserInRole 
				ON fltUserInRole.RoleId = FltRole.RoleID
			INNER JOIN fltUser 
				ON fltUser.UserName = FltUserInRole.UserName
		WHERE 
			FltUser.UserName = @UserName
			AND fltUserInRole.IsDeleted = 0
			AND FltRole.IsActive = 1
			AND FltRole.IsDeleted = 0
			AND FltUser.IsActive = 1
			AND FltUser.IsDeleted = 0)
	ORDER BY 
		Program.ProgName,
		fltRole.RoleName
	
END

SET ANSI_NULLS ON

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltUsers]
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[UserName] [NVarChar](20)
	)

	INSERT INTO #temp (
		[UserName]
	)
	SELECT
		[UserName]
	FROM [dbo].[FltUser]
	WHERE
		((@IsActiveFilter = 1 AND [FltUser].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltUser].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltUser].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltUser].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[FltUser].* FROM [dbo].[FltUser]
	INNER JOIN #temp ON 
		[dbo].[FltUser].[UserName] = #temp.[UserName]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[FltUser]
	WHERE
		((@IsActiveFilter = 1 AND [FltUser].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FltUser].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FltUser].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FltUser].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltUsersForFltRole]
	@RoleID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[FltUser].* 
	FROM FltUser 
		INNER JOIN fltUserInRole 
			ON fltUserInRole.UserName = FltUser.UserName
		INNER JOIN fltRole
			ON fltRole.RoleId = FltUserInRole.RoleId
	WHERE 
		FltRole.RoleId = @RoleId
		AND FltUserInRole.IsDeleted = 0
		AND FltUser.IsActive = 1
		AND FltUser.IsDeleted = 0
		AND FltRole.IsActive = 1
		AND FltRole.IsDeleted = 0
	
END

SET ANSI_NULLS ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFltUsersNotForFltRole]
	@RoleID int
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		[FltUser].*
	FROM
	    [FltUser]
	WHERE
		FltUser.IsActive = 1
		AND fltUser.IsDeleted = 0
		AND Fltuser.UserName NOT IN (
		SELECT 
			[FltUser].UserName
		FROM FltUser 
			INNER JOIN fltUserInRole 
				ON fltUserInRole.UserName = FltUser.UserName
			INNER JOIN fltRole
				ON fltRole.RoleId = FltUserInRole.RoleId
		WHERE 
			FltRole.RoleId = @RoleId
			AND FltUserInRole.IsDeleted = 0
			AND FltUser.IsActive = 1
			AND FltUser.IsDeleted = 0
			AND FltRole.IsActive = 1
			AND FltRole.IsDeleted = 0)
	
END

SET ANSI_NULLS ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[DocType]
	WHERE
		((@IsActiveFilter = 1 AND [DocType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [DocType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[DocType].[ProgId] = @ProgId))
	ORDER BY
		[DocType].[SortOrder],
		[DocType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[DocType].* FROM [dbo].[DocType]
	INNER JOIN #temp ON 
		[dbo].[DocType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[DocType]
	WHERE
		((@IsActiveFilter = 1 AND [DocType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [DocType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[DocType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocumentForWorkOrder]
	@WorkOrderId Int
AS
BEGIN
	SET NOCOUNT ON

	
	SELECT L.* FROM [dbo].[Document] AS L
	INNER JOIN [dbo].[WorkOrderDocXref] AS X
		ON X.[DocumentId] = L.[DocId]
	INNER JOIN [dbo].[WorkOrder] AS R
		ON R.[WorkOrderId] = X.[WorkOrderId]
	WHERE 
		R.[WorkOrderId] = @WorkOrderId
	

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocumentInfos]
	@ProgId Int = 0,
	@DocumentTypeId INT = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[DocumentId] [Int]
	)

	INSERT INTO #temp (
		[DocumentId]
	)
	SELECT
		[DocumentId]
	FROM [dbo].[DocumentInfo]
	WHERE
		((@IsDeletedFilter = 1 AND [DocumentInfo].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [DocumentInfo].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[DocumentInfo].[ProgId] = @ProgId)) AND
		((@DocumentTypeId = 0) OR (DOcumentInfo.DocumentTypeId = @DocumentTypeId))
	ORDER BY
		[DocumentInfo].[DocumentName]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		DocumentInfo.DocumentId,
		DocumentInfo.ProgId,
		DocumentInfo.DocumentTypeId,
		DocumentType.FileExtension,
		DocumentType.MimeType,
		DocumentInfo.DocumentName,
		DocumentInfo.DocumentDescription,
		DocumentInfo.DocumentSize,
		DocumentInfo.IsDeleted,
		DocumentInfo.UTCDateCreated,
		DocumentInfo.UTCDateModified,
		DocumentInfo.StampUserName,
		DocumentInfo.ConcurrencyId
	FROM [dbo].[DocumentInfo]
		INNER JOIN #temp ON 
			[dbo].[DocumentInfo].[DocumentId] = #temp.[DocumentId]
		INNER JOIN DocumentType ON
			DocumentType.DocumentTypeId = DocumentInfo.DocumentTypeId
	WHERE 
		#temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY 
		#temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocumentInfosForWorkOrder]
	@WorkOrderId Int
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		DocumentInfo.DocumentId,
		DocumentInfo.ProgId,
		DocumentInfo.DocumentTypeId,
		DocumentType.FileExtension,
		DocumentType.MimeType,
		DocumentInfo.DocumentName,
		DocumentInfo.DocumentDescription,
		DocumentInfo.DocumentSize,
		DocumentInfo.IsDeleted,
		DocumentInfo.UTCDateCreated,
		DocumentInfo.UTCDateModified,
		DocumentInfo.StampUserName,
		DocumentInfo.ConcurrencyId
	FROM 
		[dbo].[DocumentInfo]
		INNER JOIN [DocumentType] ON
			[DocumentType].[DocumentTypeId] = [DocumentInfo].[DocumentTypeId]
		INNER JOIN [dbo].[WorkOrderDocXref]
			ON [WorkOrderDocXref].[DocumentId] = [DocumentInfo].[DocumentId]
		INNER JOIN [dbo].[WorkOrder]
			ON [WorkOrder].[WorkOrderId] = [WorkOrderDocXref].[WorkOrderId]
	WHERE 
		[WorkOrder].[WorkOrderId] = @WorkOrderId AND
		[WorkOrder].[IsDeleted] = 0 AND
		[WorkOrderDocXref].[IsDeleted] = 0 AND
		[DocumentInfo].[IsDeleted] = 0
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocuments]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[DocId] [Int]
	)

	INSERT INTO #temp (
		[DocId]
	)
	SELECT
		[DocId]
	FROM [dbo].[Document]
	WHERE
		((@IsActiveFilter = 1 AND [Document].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Document].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Document].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Document].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Document].[ProgId] = @ProgId))
	ORDER BY
		[Document].[DocFileName]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Document].* FROM [dbo].[Document]
	INNER JOIN #temp ON 
		[dbo].[Document].[DocId] = #temp.[DocId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Document]
	WHERE
		((@IsActiveFilter = 1 AND [Document].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Document].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Document].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Document].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Document].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listDocumentTypes]
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[DocumentTypeId] [Int]
	)

	INSERT INTO #temp (
		[DocumentTypeId]
	)
	SELECT
		[DocumentTypeId]
	FROM [dbo].[DocumentType]
	ORDER BY
		[DocumentType].[FileExtension]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[DocumentType].* FROM [dbo].[DocumentType]
	INNER JOIN #temp ON 
		[dbo].[DocumentType].[DocumentTypeId] = #temp.[DocumentTypeId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[listPilots]
@ProgId INT=0, @IsActiveFilter INT=-1, @Start INT=-1, @Count INT=-1, @Total INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    CREATE TABLE #temp (
        tempID INT IDENTITY PRIMARY KEY,
        [Id]   INT
    );
    INSERT INTO #temp ([Id])
    SELECT   [Id]
    FROM     [dbo].[Pilot]
    WHERE    ((@IsActiveFilter = 1
               AND [Pilot].[IsActive] = 1)
              OR (@IsActiveFilter = 0
                  AND [Pilot].[IsActive] = 0)
              OR (@IsActiveFilter = -1)
              OR (@IsActiveFilter = 2))
             AND ((@ProgId = 0)
                  OR ([dbo].[Pilot].[ProgId] = @ProgId))
    ORDER BY [Pilot].[SortOrder], [Pilot].[StringValue];
    DECLARE @FromID AS INT;
    DECLARE @ToID AS INT;
    IF ((@Start = -1)
        OR (@Count = -1))
        BEGIN
            SELECT @FromID = 0;
            SELECT @ToID = 999999;
        END
    ELSE
        BEGIN
            SELECT @FromID = @Start;
            SELECT @ToID = @Start + @Count - 1;
        END
    SELECT   Pilot.Id,
             ProgId,
             CASE WHEN @IsActiveFilter = 2
                       AND IsActive = 0 THEN '(I) ' + Stringvalue ELSE StringValue END AS StringValue,
             CASE WHEN @IsActiveFilter = 2
                       AND IsActive = 0 THEN 9999 ELSE SortOrder END AS SortOrder,
             IsActive,
             ConcurrencyId
    FROM     [dbo].[Pilot]
             INNER JOIN
             #temp
             ON [dbo].[Pilot].[Id] = #temp.[Id]
    WHERE    #temp.tempID >= @FromID
             AND #temp.tempID <= @ToID
    ORDER BY SortOrder, #temp.tempID;
    DROP TABLE #temp;
    SELECT @Total = COUNT(*)
    FROM   [dbo].[Pilot]
    WHERE  ((@IsActiveFilter = 1
             AND [Pilot].[IsActive] = 1)
            OR (@IsActiveFilter = 0
                AND [Pilot].[IsActive] = 0)
            OR (@IsActiveFilter = -1)
            OR (@IsActiveFilter = 2))
           AND ((@ProgId = 0)
                OR ([dbo].[Pilot].[ProgId] = @ProgId));
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listEmissions]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Emission]
	WHERE
		((@IsActiveFilter = 1 AND [Emission].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Emission].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Emission].[ProgId] = @ProgId))
	ORDER BY
		[Emission].[SortOrder],
		[Emission].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Emission].* FROM [dbo].[Emission]
	INNER JOIN #temp ON 
		[dbo].[Emission].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Emission]
	WHERE
		((@IsActiveFilter = 1 AND [Emission].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Emission].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Emission].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listEngineeringActions]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[EngineeringAction]
	WHERE
		((@IsActiveFilter = 1 AND [EngineeringAction].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [EngineeringAction].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[EngineeringAction].[ProgId] = @ProgId))
	ORDER BY
		[EngineeringAction].[SortOrder],
		[EngineeringAction].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[EngineeringAction].* FROM [dbo].[EngineeringAction]
	INNER JOIN #temp ON 
		[dbo].[EngineeringAction].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[EngineeringAction]
	WHERE
		((@IsActiveFilter = 1 AND [EngineeringAction].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [EngineeringAction].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[EngineeringAction].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listEngineers]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Engineer]
	WHERE
		((@IsActiveFilter = 1 AND [Engineer].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Engineer].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Engineer].[ProgId] = @ProgId))
	ORDER BY
		[Engineer].[SortOrder],
		[Engineer].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Engineer].* FROM [dbo].[Engineer]
	INNER JOIN #temp ON 
		[dbo].[Engineer].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Engineer]
	WHERE
		((@IsActiveFilter = 1 AND [Engineer].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Engineer].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Engineer].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listEngineTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[EngineType]
	WHERE
		((@IsActiveFilter = 1 AND [EngineType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [EngineType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[EngineType].[ProgId] = @ProgId))
	ORDER BY
		[EngineType].[SortOrder],
		[EngineType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[EngineType].* FROM [dbo].[EngineType]
	INNER JOIN #temp ON 
		[dbo].[EngineType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[EngineType]
	WHERE
		((@IsActiveFilter = 1 AND [EngineType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [EngineType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[EngineType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[listGenericComments]
	@AircraftId INT = -1
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		SourceTable,
		SourceId,
		ParentId,
		AuthorName,
		AuthorId,
		ComponentName,
		ComponentId,
		CommentDate,
		CommentTime,
		CommentDateTime_UTC,
		CommentDateTime_Local,
		CommentRating,
		IssueCodeId,
		IssueCode,
		Comments,
		CommentGUID,
		IsDeleted,
		UTCDateCreated,
		UTCDateModified,
		StampUserName,
		ConcurrencyId
	FROM (
		SELECT
			'AircraftComment' as SourceTable,
			AircraftCommentId as SourceId,
			AircraftComment.AircraftId AS ParentId,
			Engineer.StringValue AS AuthorName,
			Engineer.ID as AuthorId,
			AircraftComponent.StringValue AS ComponentName,
			AircraftComponent.Id AS ComponentId,
			AircraftComment.CommentDate,
			'' AS CommentTime,
			NULL AS CommentDateTime_UTC,
			NULL AS CommentDateTime_Local,
			AircraftComment.CommentRating,
			AircraftComment.IssueCodeId,
			ISNULL(IssueCode.StringValue, '') AS IssueCode,
			AircraftComment.Comments,
			AircraftComment.AircraftCommentGUID AS CommentGUID,
			AircraftComment.IsDeleted,
			AircraftComment.UTCDateCreated,
			AircraftComment.UTCDateModified,
			AircraftComment.StampUserName,
			AircraftComment.ConcurrencyId
		FROM
			Aircraft
			INNER JOIN AircraftComment ON AircraftComment.AircraftId = Aircraft.AircraftId
			INNER JOIN Engineer ON Engineer.ID = AircraftComment.EngineerId
			INNER JOIN AircraftComponent ON AircraftComponent.id = AircraftComment.ComponentId
			LEFT OUTER JOIN IssueCode ON IssueCode.ID = AircraftComment.IssueCodeId
		WHERE
			(Aircraft.AircraftId = @AircraftId)
			AND Aircraft.IsDeleted = 0
			AND AircraftComment.IsDeleted = 0
		UNION
		SELECT
			'WOEngineerComment' as SourceTable,
			WOEngineerComment.EngineerCommentsId as SourceId,
			WOEngineerComment.WorkOrderId AS ParentId,
			Engineer.StringValue AS AuthorName,
			Engineer.ID as AuthorId,
			AircraftComponent.StringValue AS ComponentName,
			AircraftComponent.Id AS ComponentId,
			WOEngineerComment.CommentDate,
			'' AS CommentTime,
			NULL AS CommentDateTime_UTC,
			NULL AS CommentDateTime_Local,
			WOEngineerComment.CommentRating,
			0 AS IssueCodeId,
			'' AS IssueCode,
			WOEngineerComment.Comments,
			WOEngineerComment.EngineerCommentGUID AS CommentGUID,
			WOEngineerComment.IsDeleted,
			WOEngineerComment.UTCDateCreated,
			WOEngineerComment.UTCDateModified,
			WOEngineerComment.StampUserName,
			WOEngineerComment.ConcurrencyId
		FROM
			WorkOrder
			INNER JOIN Aircraft ON Aircraft.AircraftId = WorkOrder.AircraftId
			INNER JOIN WOEngineerComment ON WOEngineerComment.WorkOrderId = WorkOrder.WorkOrderId
			INNER JOIN Engineer ON Engineer.id = WorkOrder.EngineerId
			INNER JOIN AircraftComponent ON AircraftComponent.Id = WOEngineerComment.ComponentId
		WHERE
			(Aircraft.AircraftId = @AircraftId)
			AND Aircraft.IsDeleted = 0
			AND WorkOrder.IsDeleted = 0
			AND dbo.WOEngineerComment.IsDeleted = 0
		UNION
		SELECT
			'WOMechanicComment' as SourceTable,
			WOMechanicComment.WOMechanicCommentsId as SourceId,
			WOMechanicComment.WorkOrderId AS ParentId,
			Mechanic.StringValue AS AuthorName,
			Mechanic.ID as AuthorId,
			AircraftComponent.StringValue AS ComponentName,
			AircraftComponent.Id AS ComponentId,
			WOMechanicComment.CommentDate,
			'' AS CommentTime,
			NULL AS CommentDateTime_UTC,
			NULL AS CommentDateTIME_Local,
			WOMechanicComment.CommentRating,
			0 AS IssueCodeId,
			'' AS IssueCode,
			WOMechanicComment.Comments,
			WOMechanicComment.WOMechanicCommentGUID AS CommentGUID,
			WOMechanicComment.IsDeleted,
			WOMechanicComment.UTCDateCreated,
			WOMechanicComment.UTCDateModified,
			WOMechanicComment.StampUserName,
			WOMechanicComment.ConcurrencyId
		FROM
			WorkOrder
			INNER JOIN Aircraft ON Aircraft.AircraftId = WorkOrder.AircraftId
			INNER JOIN WOMechanicComment ON WOMechanicComment.WorkOrderId = WorkOrder.WorkOrderId
			INNER JOIN Mechanic ON Mechanic.id = WorkOrder.MechanicId
			INNER JOIN AircraftComponent ON AircraftComponent.Id = WOMechanicComment.ComponentId
		WHERE
			(Aircraft.AircraftId = @AircraftId)
			AND Aircraft.IsDeleted = 0
			AND WorkOrder.IsDeleted = 0
			AND dbo.WOMechanicComment.IsDeleted = 0
		UNION
		SELECT
			'FlightComment' as SourceTable,
			TC.FlightCommentID as SourceId,
			TC.FlightId AS ParentId,
			D.StringValue AS AuthorName,
			D.ID as AuthorId,
			VC.StringValue as ComponentName,
			VC.Id AS ComponentId,
			TC.CommentDateTime_Local as CommentDate,
			TC.CommentTime,
			TC.CommentDateTime_UTC,
			TC.CommentDateTime_Local,
			TC.CommentRating,
			TC.IssueCodeId,
			ISNULL(IssueCode.StringValue, '') AS IssueCode,
			TC.PilotComment as Comments,
			TC.FlightCommentGUID AS CommentGUID,
			TC.IsDeleted,
			TC.UTCDateCreated,
			TC.UTCDateModified,
			TC.StampUserName,
			TC.ConcurrencyId
		FROM
			Aircraft AS V
			INNER JOIN Flight AS T ON
				T.AircraftId = V.AircraftId
			INNER JOIN Pilot AS D ON
				D.Id = T.PilotId
			INNER JOIN FlightComment AS TC ON 
				TC.FlightId = T.FlightId
			INNER JOIN AircraftComponent AS VC ON
				VC.Id = TC.ComponentId
			LEFT OUTER JOIN IssueCode ON
				IssueCode.Id = TC.IssueCodeId
		WHERE
			(V.AircraftId = @AircraftId)
			AND V.IsDeleted = 0
			AND T.IsDeleted = 0
			AND TC.IsDeleted = 0
	) AS VC
	ORDER BY
		CommentDate DESC,
		UTCDateModified DESC

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listIssueCodes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[IssueCode]
	WHERE
		((@IsActiveFilter = 1 AND [IssueCode].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [IssueCode].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[IssueCode].[ProgId] = @ProgId))
	ORDER BY
		[IssueCode].[SortOrder],
		[IssueCode].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[IssueCode].* FROM [dbo].[IssueCode]
	INNER JOIN #temp ON 
		[dbo].[IssueCode].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[IssueCode]
	WHERE
		((@IsActiveFilter = 1 AND [IssueCode].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [IssueCode].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[IssueCode].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLengthUnits]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[LengthUnit]
	WHERE
		((@IsActiveFilter = 1 AND [LengthUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LengthUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LengthUnit].[ProgId] = @ProgId))
	ORDER BY
		[LengthUnit].[SortOrder],
		[LengthUnit].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[LengthUnit].* FROM [dbo].[LengthUnit]
	INNER JOIN #temp ON 
		[dbo].[LengthUnit].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LengthUnit]
	WHERE
		((@IsActiveFilter = 1 AND [LengthUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LengthUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LengthUnit].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLiquidFills]
	@FlightId Int = 0,
	@LiquidGroupId Int = 0,
	@LiquidTypeId Int = 0,
	@LiquidUnitId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[LiquidFillId] [Int]
	)

	INSERT INTO #temp (
		[LiquidFillId]
	)
	SELECT
		[LiquidFillId]
	FROM [dbo].[LiquidFill]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidFill].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidFill].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [LiquidFill].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [LiquidFill].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[LiquidFill].[FlightId] = @FlightId)) AND 
		((@LiquidGroupId = 0) OR ([dbo].[LiquidFill].[LiquidGroupId] = @LiquidGroupId)) AND 
		((@LiquidTypeId = 0) OR ([dbo].[LiquidFill].[LiquidTypeId] = @LiquidTypeId)) AND 
		((@LiquidUnitId = 0) OR ([dbo].[LiquidFill].[LiquidUnitId] = @LiquidUnitId))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[LiquidFill].*,
		[dbo].[LiquidGroup].[StringValue] AS LiquidGroupName,
		[dbo].[LiquidType].[StringValue] AS LiquidTypeName,
		[dbo].[LiquidUnit].[StringValue] AS LiquidUnitName
	FROM 
		[dbo].[LiquidFill]
		INNER JOIN #temp ON 
			[dbo].[LiquidFill].[LiquidFillId] = #temp.[LiquidFillId]
		INNER JOIN [dbo].[LiquidGroup] ON
			[dbo].[LiquidGroup].[Id] = [dbo].[LiquidFill].[LiquidGroupId]
		INNER JOIN [dbo].[LiquidType] ON
			[dbo].[LiquidType].[Id] = [dbo].[LiquidFill].[LiquidTypeId]
		INNER JOIN [dbo].[LiquidUnit] ON
			[dbo].[LiquidUnit].[Id] = [dbo].[LiquidFill].[LiquidUnitId]
	WHERE 
		#temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY 
		#temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LiquidFill]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidFill].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidFill].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [LiquidFill].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [LiquidFill].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[LiquidFill].[FlightId] = @FlightId)) AND 
		((@LiquidGroupId = 0) OR ([dbo].[LiquidFill].[LiquidGroupId] = @LiquidGroupId)) AND 
		((@LiquidTypeId = 0) OR ([dbo].[LiquidFill].[LiquidTypeId] = @LiquidTypeId)) AND 
		((@LiquidUnitId = 0) OR ([dbo].[LiquidFill].[LiquidUnitId] = @LiquidUnitId))

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLiquidGroups]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[ID] [Int]
	)

	INSERT INTO #temp (
		[ID]
	)
	SELECT
		[ID]
	FROM [dbo].[LiquidGroup]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidGroup].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidGroup].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidGroup].[ProgId] = @ProgId))
	ORDER BY
		[LiquidGroup].[SortOrder],
		[LiquidGroup].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[LiquidGroup].* FROM [dbo].[LiquidGroup]
	INNER JOIN #temp ON 
		[dbo].[LiquidGroup].[ID] = #temp.[ID]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LiquidGroup]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidGroup].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidGroup].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidGroup].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLiquidTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[LiquidType]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidType].[ProgId] = @ProgId))
	ORDER BY
		[LiquidType].[SortOrder],
		[LiquidType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[LiquidType].* FROM [dbo].[LiquidType]
	INNER JOIN #temp ON 
		[dbo].[LiquidType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LiquidType]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLiquidUnits]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[LiquidUnit]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidUnit].[ProgId] = @ProgId))
	ORDER BY
		[LiquidUnit].[SortOrder],
		[LiquidUnit].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[LiquidUnit].* FROM [dbo].[LiquidUnit]
	INNER JOIN #temp ON 
		[dbo].[LiquidUnit].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LiquidUnit]
	WHERE
		((@IsActiveFilter = 1 AND [LiquidUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LiquidUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LiquidUnit].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLocations]
	@ProgId Int = 0,
	@TimeZoneId INT = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[LocactionId] [Int]
	)

	INSERT INTO #temp (
		[LocactionId]
	)
	SELECT
		[LocactionId]
	FROM [dbo].[Location]
	WHERE
		((@IsActiveFilter = 1 AND [Location].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Location].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Location].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Location].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Location].[ProgId] = @ProgId)) AND 
		((@TimeZoneId = 0) OR ([dbo].[Location].[TimeZoneId] = @TimeZoneId))
	ORDER BY
		[Location].[LocationName]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[Location].* ,
		UTCTimeZone.TimeZoneCode,
		UTCTimeZone.TimeZoneOffset,
		UTCTimeZone.DayLightSavingTime,
		UTCTimeZone.DaylightSavingOffset,
		UTCTimeZone.DaylightSavingBegin,
		UTCTimeZone.DaylightSavingEnd
	FROM [dbo].[Location]
		INNER JOIN #temp ON 
			[dbo].[Location].[LocactionId] = #temp.[LocactionId]
		INNER JOIN [dbo].[UTCTimeZone] ON
			[dbo].[UTCTimeZone].TimeZoneId = [dbo].[Location].TimeZoneId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listManufacturers]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Manufacturer]
	WHERE
		((@IsActiveFilter = 1 AND [Manufacturer].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Manufacturer].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Manufacturer].[ProgId] = @ProgId))
	ORDER BY
		[Manufacturer].[SortOrder],
		[Manufacturer].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Manufacturer].* FROM [dbo].[Manufacturer]
	INNER JOIN #temp ON 
		[dbo].[Manufacturer].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Manufacturer]
	WHERE
		((@IsActiveFilter = 1 AND [Manufacturer].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Manufacturer].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Manufacturer].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listMaturityDefs]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[MaturityDef]
	WHERE
		((@IsActiveFilter = 1 AND [MaturityDef].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [MaturityDef].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[MaturityDef].[ProgId] = @ProgId))
	ORDER BY
		[MaturityDef].[SortOrder],
		[MaturityDef].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[MaturityDef].* FROM [dbo].[MaturityDef]
	INNER JOIN #temp ON 
		[dbo].[MaturityDef].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[MaturityDef]
	WHERE
		((@IsActiveFilter = 1 AND [MaturityDef].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [MaturityDef].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[MaturityDef].[ProgId] = @ProgId))

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listMechActions]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[MechAction]
	WHERE
		((@IsActiveFilter = 1 AND [MechAction].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [MechAction].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[MechAction].[ProgId] = @ProgId))
	ORDER BY
		[MechAction].[SortOrder],
		[MechAction].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[MechAction].* FROM [dbo].[MechAction]
	INNER JOIN #temp ON 
		[dbo].[MechAction].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[MechAction]
	WHERE
		((@IsActiveFilter = 1 AND [MechAction].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [MechAction].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[MechAction].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listModels]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Model]
	WHERE
		((@IsActiveFilter = 1 AND [Model].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Model].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Model].[ProgId] = @ProgId))
	ORDER BY
		[Model].[SortOrder],
		[Model].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Model].* FROM [dbo].[Model]
	INNER JOIN #temp ON 
		[dbo].[Model].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Model]
	WHERE
		((@IsActiveFilter = 1 AND [Model].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Model].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Model].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listPrograms]
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[ProgID] [Int]
	)

	INSERT INTO #temp (
		[ProgID]
	)
	SELECT
		[ProgID]
	FROM [dbo].[Program]
	WHERE
		((@IsActiveFilter = 1 AND [Program].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Program].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Program].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Program].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))
	ORDER BY
		[Program].[ProgName],
		[Program].[ProgVersion]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Program].* FROM [dbo].[Program]
	INNER JOIN #temp ON 
		[dbo].[Program].[ProgID] = #temp.[ProgID]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Program]
	WHERE
		((@IsActiveFilter = 1 AND [Program].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Program].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Program].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Program].[IsDeleted] = 0) OR (@IsDeletedFilter = -1))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listRouteForRegion]
	@Id Int
AS
BEGIN
	SET NOCOUNT ON

	
	SELECT L.* FROM [dbo].[Route] AS L
	INNER JOIN [dbo].[RouteRegion] AS X
		ON X.[RouteId] = L.[RouteId]
	INNER JOIN [dbo].[Region] AS R
		ON R.[Id] = X.[RegionId]
	WHERE 
		R.[Id] = @Id
	

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listRoutes]
	@ProgId INT,
	@LocationId Int = 0,
	@TimeZoneId INT = 0,
	@IsActiveFilter int = 1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[RouteId] [Int]
	)

	INSERT INTO #temp (
		[RouteId]
	)
	SELECT
		[RouteId]
	FROM [dbo].[Route]
		INNER JOIN [dbo].[Location] ON dbo.[Route].LocationId = dbo.Location.LocactionId
		INNER JOIN [dbo].Program ON dbo.Location.ProgId = dbo.Program.ProgID
	WHERE
		((@IsActiveFilter = 1 AND [Route].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Route].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Route].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Route].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((Location.IsActive = 1)) AND
		((Location.IsDeleted = 0)) AND
		((@ProgId = Program.ProgId)) AND
		((@LocationId = 0) OR ([dbo].[Route].[LocationId] = @LocationId)) AND 
		((@TimeZoneId = 0) OR ([dbo].[Route].[TimeZoneId] = @TimeZoneId))
	ORDER BY
		[Route].[Name]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[Route].*,
		[dbo].[UTCTimeZone].TimeZoneCode
	FROM 
		[dbo].[Route]
		INNER JOIN #temp ON 
			[dbo].[Route].[RouteId] = #temp.[RouteId]
		INNER JOIN [dbo].[UTCTimeZone] ON
			[dbo].[UTCTimeZone].TimeZoneId = [dbo].[Route].TimeZoneId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listMechanics]
	@Progid INT = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Mechanic]
	WHERE
		((@IsActiveFilter = 1 AND [Mechanic].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Mechanic].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND
		((@ProgId = 0) OR ([dbo].[Mechanic].[ProgId] = @ProgId))
	ORDER BY
		[Mechanic].[SortOrder],
		[Mechanic].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Mechanic].* FROM [dbo].[Mechanic]
	INNER JOIN #temp ON 
		[dbo].[Mechanic].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Mechanic]
	WHERE
		((@IsActiveFilter = 1 AND [Mechanic].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Mechanic].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND
		((@ProgId = 0) OR ([dbo].[Mechanic].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listRegionForRoute]
	@RouteId Int
AS
BEGIN
	SET NOCOUNT ON

	
	SELECT L.* FROM [dbo].[Region] AS L
	INNER JOIN [dbo].[RouteRegion] AS X
		ON X.[RegionId] = L.[Id]
	INNER JOIN [dbo].[Route] AS R
		ON R.[RouteId] = X.[RouteId]
	WHERE 
		R.[RouteId] = @RouteId
	

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listRegions]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Region]
	WHERE
		((@IsActiveFilter = 1 AND [Region].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Region].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Region].[ProgId] = @ProgId))
	ORDER BY
		[Region].[Code],
		[Region].[Name]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Region].* FROM [dbo].[Region]
	INNER JOIN #temp ON 
		[dbo].[Region].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Region]
	WHERE
		((@IsActiveFilter = 1 AND [Region].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Region].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Region].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listTests]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Test]
	WHERE
		((@IsActiveFilter = 1 AND [Test].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Test].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Test].[ProgId] = @ProgId))
	ORDER BY
		[Test].[StringValue],
		[Test].[SortOrder]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Test].* FROM [dbo].[Test]
	INNER JOIN #temp ON 
		[dbo].[Test].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Test]
	WHERE
		((@IsActiveFilter = 1 AND [Test].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Test].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Test].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listTestsForAircraft]
	@AircraftId INT,
	@ProgId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[Test].*
	FROM Test 
		INNER JOIN AircraftTestXref 
			ON AircraftTestXref.Testid = Test.Id
		INNER JOIN Aircraft
			ON Aircraft.AircraftId = AircraftTestXref.AircraftId
	WHERE 
		Aircraft.AircraftId = @AircraftId
		AND Aircraft.ProgId = @ProgId
		AND Aircraft.IsActive = 1
		AND Aircraft.IsDeleted = 0
		AND AircraftTestXref.IsDeleted = 0
		AND Test.ProgId = @ProgId
		AND Test.IsActive = 1
	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listTestsNotForAircraft]
	@AircraftId INT,
	@ProgId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		[Test].*
	FROM
	    [Test]
	WHERE
		Test.IsActive = 1
		AND Test.ProgId = @ProgId
		AND Test.Id NOT IN (
		SELECT 
			[Test].Id
	FROM Test 
		INNER JOIN AircraftTestXref 
			ON AircraftTestXref.Testid = Test.Id
		INNER JOIN Aircraft
			ON Aircraft.AircraftId = AircraftTestXref.AircraftId
	WHERE 
		Aircraft.AircraftId = @AircraftId
		AND Aircraft.ProgId = @ProgId
		AND Aircraft.IsActive = 1
		AND Aircraft.IsDeleted = 0
		AND AircraftTestXref.IsDeleted = 0
		AND Test.ProgId = @ProgId
		AND Test.IsActive = 1 )
	
	
END

SET ANSI_NULLS ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listLoadingConfigs]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[LoadingConfig]
	WHERE
		((@IsActiveFilter = 1 AND [LoadingConfig].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LoadingConfig].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LoadingConfig].[ProgId] = @ProgId))
	ORDER BY
		[LoadingConfig].[SortOrder],
		[LoadingConfig].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[LoadingConfig].* FROM [dbo].[LoadingConfig]
	INNER JOIN #temp ON 
		[dbo].[LoadingConfig].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[LoadingConfig]
	WHERE
		((@IsActiveFilter = 1 AND [LoadingConfig].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [LoadingConfig].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[LoadingConfig].[ProgId] = @ProgId))

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listPropellers]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[Propeller]
	WHERE
		((@IsActiveFilter = 1 AND [Propeller].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Propeller].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Propeller].[ProgId] = @ProgId))
	ORDER BY
		[Propeller].[SortOrder],
		[Propeller].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Propeller].* FROM [dbo].[Propeller]
	INNER JOIN #temp ON 
		[dbo].[Propeller].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[Propeller]
	WHERE
		((@IsActiveFilter = 1 AND [Propeller].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Propeller].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Propeller].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listPropellerTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[PropellerType]
	WHERE
		((@IsActiveFilter = 1 AND [PropellerType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [PropellerType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[PropellerType].[ProgId] = @ProgId))
	ORDER BY
		[PropellerType].[SortOrder],
		[PropellerType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[PropellerType].* FROM [dbo].[PropellerType]
	INNER JOIN #temp ON 
		[dbo].[PropellerType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[PropellerType]
	WHERE
		((@IsActiveFilter = 1 AND [PropellerType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [PropellerType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[PropellerType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFlightComments]
	@FlightId Int = 0,
	@ComponentId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[FlightCommentId] [Int]
	)

	INSERT INTO #temp (
		[FlightCommentId]
	)
	SELECT
		[FlightCommentId]
	FROM 
		[dbo].[FlightComment]
		INNER JOIN [dbo].[AircraftComponent] ON
			[dbo].[AircraftComponent].Id = [dbo].[FlightComment].ComponentId
	WHERE
		((@IsActiveFilter = 1 AND [FlightComment].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FlightComment].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FlightComment].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FlightComment].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[FlightComment].[FlightId] = @FlightId)) AND 
		((@ComponentId = 0) OR ([dbo].[FlightComment].[ComponentId] = @ComponentId))
	ORDER BY
		[FlightComment].[CommentTime]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[FlightComment].*,
		[dbo].[AircraftComponent].StringValue AS ComponentName,
		ISNULL(IssueCode.StringValue, '') AS IssueCode
	FROM 
		[dbo].[FlightComment]
		INNER JOIN #temp ON 
			[dbo].[FlightComment].[FlightCommentId] = #temp.[FlightCommentId]
		INNER JOIN [dbo].[AircraftComponent] ON
			[dbo].[AircraftComponent].Id = [dbo].[FlightComment].ComponentId			
		LEFT OUTER JOIN IssueCode ON
			IssueCode.Id = FlightComment.IssueCodeId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT 
		@Total = COUNT(*) 
	FROM 
		[dbo].[FlightComment]
		INNER JOIN [dbo].[AircraftComponent] ON
			[dbo].[AircraftComponent].Id = [dbo].[FlightComment].ComponentId
	WHERE
		((@IsActiveFilter = 1 AND [FlightComment].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FlightComment].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FlightComment].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FlightComment].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[FlightComment].[FlightId] = @FlightId)) AND 
		((@ComponentId = 0) OR ([dbo].[FlightComment].[ComponentId] = @ComponentId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFlightInfos]
	@LocationId INT = 0,
	@AircraftId INT = 0,
	@AircraftTextMatch NVARCHAR(20) = '',
	@SchedRouteId Int = 0,
	@FlownRouteId INT = 0,
	@WorkShiftIndicatorId Int = 0,
	@FilterDateBegin VARCHAR(20) = '',
	@FilterDateEnd VARCHAR(20) = '',
	@IsApprovedFilter INT = -1,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @DateBegin DATETIME
	DECLARE @DateEnd DATETIME
	IF (LEN(@FilterDateBegin) <> 0)
		SELECT @DateBegin = CONVERT(DATETIME, @FilterDateBegin)
	ELSE
		SELECT @DateBegin = CONVERT(DATETIME, '1900-01-01')
		
	IF (LEN(@FilterDateEnd) <> 0)
		SELECT @DateEnd = CONVERT(DATETIME, @FilterDateEnd)
	ELSE
		SELECT @DateEnd = CONVERT(DATETIME, '2100-12-31')
	

	

	SELECT
		[dbo].[Flight].FlightId,
		[dbo].[Flight].LocationId,
		[dbo].[Location].[LocationName],
		[dbo].[Flight].[FlightDate],
		[dbo].[WorkShift].[StringValue] AS WorkShift,
		[dbo].[Aircraft].[MSN],
		[dbo].[Aircraft].[SerialNbr],
		[dbo].[Aircraft].[RegistrationNbr],
		[dbo].[Flight].[FlightDateSort],
		[dbo].[Flight].[DepartureTime],
		[dbo].[Flight].[Returntime],
		[dbo].[Flight].[AirframeHoursStart],
		[dbo].[Flight].[AirframeHoursEnd],
		[dbo].[Flight].[HobbsEnd] - [dbo].[Flight].[HobbsStart] AS FlightUsage,
		[dbo].[Flight].[IsApproved],
		[dbo].[Flight].[IsActive],
		[dbo].[Flight].[IsDeleted],
		[dbo].[Flight].[UTCDateCreated],
		[dbo].[Flight].[UTCDateModified]
	FROM 
		[dbo].[Flight]
		INNER JOIN [dbo].[Location] ON
			[dbo].[Location].[LocactionId] = [dbo].[Flight].[LocationId]
		INNER JOIN [dbo].[WorkShift] on
			[dbo].[WorkShift].[Id] = [dbo].[Flight].[WorkShiftIndicatorId]
		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[Flight].[AircraftId]
	WHERE
	    ((@IsApprovedFilter = 1 AND [Flight].[IsApproved] = 1) OR (@IsApprovedFilter = 0 AND [Flight].[IsApproved] = 0) OR (@IsApprovedFilter = -1)) AND
		((@IsActiveFilter = 1 AND [Flight].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Flight].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Flight].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Flight].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@AircraftId = 0) OR ([dbo].[Flight].[AircraftId] = @AircraftId)) AND 
		((@AircraftTextMatch = '') OR 
		 ([dbo].[Aircraft].[SerialNbr] LIKE @AircraftTextMatch) OR
		 ([dbo].[Aircraft].[RegistrationNbr] LIKE @AircraftTextMatch) OR 
		 ([dbo].[Aircraft].[MSN] LIKE @AircraftTextMatch) OR 
		 ([dbo].[Aircraft].[ShipNbr] LIKE @AircraftTextMatch)) AND
		((@LocationId = 0) OR ([dbo].[Flight].[LocationId] = @LocationId)) AND 
		((@SchedRouteId = 0) OR ([dbo].[Flight].[SchedRouteId] = @SchedRouteId)) AND 
		((@FlownRouteId = 0) OR ([dbo].[Flight].[FlownRouteId] = @FlownRouteId)) AND 
		((@WorkShiftIndicatorId = 0) OR ([dbo].[Flight].[WorkShiftIndicatorId] = @WorkShiftIndicatorId)) AND
		((CONVERT(DATETIME, Flight.FlightDate) >= @DateBegin) AND (CONVERT(DATETIME, Flight.Flightdate) <= @DateEnd))
	ORDER BY
		Aircraft.MSN,
		[Flight].[AirframeHoursEnd] DESC,
		dbo.Flight.flightdatesort DESC

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFlightEngineRuns]
	@FlightId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[FlightEngineRunId] [Int]
	)

	INSERT INTO #temp (
		[FlightEngineRunId]
	)
	SELECT
		[FlightEngineRunId]
	FROM [dbo].[FlightEngineRun]
	WHERE
		((@IsDeletedFilter = 1 AND [FlightEngineRun].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FlightEngineRun].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[FlightEngineRun].[FlightId] = @FlightId))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[FlightEngineRun].*
	FROM [dbo].[FlightEngineRun]
		INNER JOIN #temp ON 
			[dbo].[FlightEngineRun].[FlightEngineRunId] = #temp.[FlightEngineRunId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY FlightEngineRun.FlightEngineRunId

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFlights]
	@AircraftId INT = 0,
	@LocationId INT = 0,
	@SchedRouteId Int = 0,
	@FlownRouteId INT = 0,
	@WorkShiftIndicatorId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[FlightId] [Int]
	)

	INSERT INTO #temp (
		[FlightId]
	)
	SELECT
		[FlightId]
	FROM [dbo].[Flight]
	WHERE
		((@IsActiveFilter = 1 AND [Flight].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Flight].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Flight].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Flight].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@AircraftId = 0) OR ([dbo].[Flight].[AircraftId] = @AircraftId)) AND 
		((@LocationId = 0) OR ([dbo].[Flight].[LocationId] = @LocationId)) AND 
		((@SchedRouteId = 0) OR ([dbo].[Flight].[SchedRouteId] = @SchedRouteId)) AND 
		((@FlownRouteId = 0) OR ([dbo].[Flight].[FlownRouteId] = @FlownRouteId)) AND 
		((@WorkShiftIndicatorId = 0) OR ([dbo].[Flight].[WorkShiftIndicatorId] = @WorkShiftIndicatorId))
	ORDER BY
		[Flight].[FlightDate]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Flight].* FROM [dbo].[Flight]
	INNER JOIN #temp ON 
		[dbo].[Flight].[FlightId] = #temp.[FlightId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFlightRegionTimes]
	@FlightId Int = 0,
	@RegionId Int = 0,
	@IsActiveFilter int = 1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[FlightRegionTimeId] [Int]
	)

	INSERT INTO #temp (
		[FlightRegionTimeId]
	)
	SELECT
		[FlightRegionTimeId]
	FROM [dbo].[FlightRegionTime]
		INNER JOIN [Region] ON 
			dbo.Region.ID = dbo.FlightRegionTime.RegionId 
	WHERE
		((@IsActiveFilter = 1 AND [FlightRegionTime].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FlightRegionTime].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FlightRegionTime].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FlightRegionTime].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[FlightRegionTime].[FlightId] = @FlightId)) AND 
		((@RegionId = 0) OR ([dbo].[FlightRegionTime].[RegionId] = @RegionId))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[FlightRegionTime].*,
		[dbo].[Region].Code AS RegionCode
	FROM [dbo].[FlightRegionTime]
		INNER JOIN #temp ON 
			[dbo].[FlightRegionTime].[FlightRegionTimeId] = #temp.[FlightRegionTimeId]
		INNER JOIN [Region] ON 
			dbo.Region.ID = dbo.FlightRegionTime.RegionId 
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) 
	FROM 
		[dbo].[FlightRegionTime]
		INNER JOIN [Region] ON 
			dbo.Region.ID = dbo.FlightRegionTime.FlightRegionTimeId 
	WHERE
		((@IsActiveFilter = 1 AND [FlightRegionTime].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [FlightRegionTime].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [FlightRegionTime].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [FlightRegionTime].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@FlightId = 0) OR ([dbo].[FlightRegionTime].[FlightId] = @FlightId)) AND 
		((@RegionId = 0) OR ([dbo].[FlightRegionTime].[RegionId] = @RegionId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listUTCTimeZones]
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[TimeZoneId] Int
	)

	INSERT INTO #temp (
		[TimeZoneId]
	)
	SELECT
		[TimeZoneId]
	FROM [dbo].[UTCTimeZone]
	WHERE
		((@IsActiveFilter = 1 AND [UTCTimeZone].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [UTCTimeZone].[IsActive] = 0) OR (@IsActiveFilter = -1))
	ORDER BY
		[UTCTimeZone].[TimeZoneOffset]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[UTCTimeZone].* FROM [dbo].[UTCTimeZone]
	INNER JOIN #temp ON 
		[dbo].[UTCTimeZone].[TimeZoneId] = #temp.[TimeZoneId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftComments]
	@AircraftId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[AircraftCommentId] [Int]
	)

	INSERT INTO #temp (
		[AircraftCommentId]
	)
	SELECT
		[AircraftCommentId]
	FROM [dbo].[AircraftComment]
	WHERE
		((@IsDeletedFilter = 1 AND [AircraftComment].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [AircraftComment].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[AircraftComment].[AircraftId] = @AircraftId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[AircraftComment].*,
		Engineer.StringValue AS EngineerName,
		AircraftComponent.StringValue AS Component,
		ISNULL(IssueCode.StringValue, '') AS IssueCode
	FROM 
		[dbo].[AircraftComment]
	INNER JOIN #temp ON 
		[dbo].[AircraftComment].[AircraftCommentId] = #temp.[AircraftCommentId]
	INNER JOIN [Engineer] ON
		Engineer.id = AircraftComment.EngineerId
	INNER JOIN dbo.AircraftComponent ON
		dbo.AircraftComponent.Id = dbo.AircraftComment.ComponentId
	LEFT OUTER JOIN IssueCode ON
		dbo.IssueCode.Id = dbo.AircraftComment.IssueCodeId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftComponents]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[AircraftComponent]
	WHERE
		((@IsActiveFilter = 1 AND [AircraftComponent].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AircraftComponent].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[AircraftComponent].[ProgId] = @ProgId))
	ORDER BY
		[AircraftComponent].[StringValue],
		[AircraftComponent].[SortOrder]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[AircraftComponent].* FROM [dbo].[AircraftComponent]
	INNER JOIN #temp ON 
		[dbo].[AircraftComponent].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[AircraftComponent]
	WHERE
		((@IsActiveFilter = 1 AND [AircraftComponent].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AircraftComponent].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[AircraftComponent].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftConfigurations]
	@AircraftId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[AircraftConfigurationId] [Int]
	)

	INSERT INTO #temp (
		[AircraftConfigurationId]
	)
	SELECT
		[AircraftConfigurationId]
	FROM [dbo].[AircraftConfiguration]
	WHERE
		((@IsDeletedFilter = 1 AND [AircraftConfiguration].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [AircraftConfiguration].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@AircraftId = 0) OR ([dbo].[AircraftConfiguration].[AircraftId] = @AircraftId))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[AircraftConfiguration].*,
		[dbo].[LengthUnit].[StringValue] AS [MeterUnit],
		[dbo].[Model].[StringValue] AS [ModelName],
		[dbo].[EngineType].[StringValue] AS [EngineType],
		[EngineEmissionDef].[StringValue] AS [EngineEmission],
		[EngineMaturityDef].[StringValue] AS [EngineMaturity],
		[dbo].[APUType].[StringValue] AS [APUType],
		[APUEmissionDef].StringValue AS [APUEmission],
		[APUMaturityDef].StringValue AS [APUMaturity],
		[dbo].[PropellerType].[StringValue] AS [PropellerType]		
	FROM [dbo].[AircraftConfiguration]
		INNER JOIN #temp ON 
			[dbo].[AircraftConfiguration].[AircraftConfigurationId] = #temp.[AircraftConfigurationId]
		INNER JOIN [dbo].[EngineType] ON
			[dbo].[EngineType].[Id] = [dbo].[AircraftConfiguration].[EngineTypeId]
		INNER JOIN [dbo].[Emission] AS [EngineEmissionDef] ON
			[EngineEmissionDef].[Id] = [dbo].[AircraftConfiguration].[EngineEmissionId]
		INNER JOIN [dbo].[MaturityDef] AS [EngineMaturityDef] ON
			[EngineMaturityDef].[Id] = [dbo].[AircraftConfiguration].[EngineMaturityId]
		INNER JOIN [dbo].[APUType] ON
			[dbo].[APUType].[Id] = [dbo].[AircraftConfiguration].[APUTypeId]
		INNER JOIN [dbo].[Emission] AS [APUEmissionDef] ON
			[APUEmissionDef].[Id] = [dbo].[AircraftConfiguration].[APUEmissionId]
		INNER JOIN [dbo].[MaturityDef] AS [APUMaturityDef] ON
			[APUMaturityDef].[Id] = [dbo].[AircraftConfiguration].[APUMaturityId]
		INNER JOIN [dbo].[PropellerType] ON
			[dbo].[PropellerType].[Id] = [dbo].[AircraftConfiguration].[PropellerTypeId]
		INNER JOIN [dbo].[LengthUnit] ON
			[dbo].[LengthUnit].[Id] = [dbo].[AircraftConfiguration].[MeterUnitId]
		INNER JOIN [dbo].[Model] ON
			[dbo].[Model].[ID] = [dbo].[AircraftConfiguration].[ModelId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[listAircraftInfos]
@ProgId INT=0, @CurrentLocationId INT=0, @AircraftTextMatch NVARCHAR (30)='', @IsActiveFilter INT=-1, @IsDeletedFilter INT=0, @Start INT=-1, @Count INT=-1, @Total INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    CREATE TABLE #temp (
        tempID      INT IDENTITY PRIMARY KEY,
        [AircraftId] INT
    );
    INSERT INTO #temp ([AircraftId])
    SELECT   [AircraftId]
    FROM     [dbo].[Aircraft]
    WHERE    ((@IsActiveFilter = 1
               AND [Aircraft].[IsActive] = 1)
              OR (@IsActiveFilter = 0
                  AND [Aircraft].[IsActive] = 0)
              OR (@IsActiveFilter = -1))
             AND ((@IsDeletedFilter = 1
                   AND [Aircraft].[IsDeleted] = 1)
                  OR (@IsDeletedFilter = 0
                      AND [Aircraft].[IsDeleted] = 0)
                  OR (@IsDeletedFilter = -1))
             AND ((@ProgId = 0)
                  OR ([dbo].[Aircraft].[ProgId] = @ProgId))
             AND ((@CurrentLocationId = 0)
                  OR (isnull([dbo].[Aircraft].[CurrentLocationId], 0) = @CurrentLocationId))
             AND ((@AircraftTextMatch = '')
                  OR ([dbo].[Aircraft].[SerialNbr] LIKE @AircraftTextMatch)
                  OR ([dbo].[Aircraft].[RegistrationNbr] LIKE @AircraftTextMatch)
                  OR ([dbo].[Aircraft].[MSN] LIKE @AircraftTextMatch)
                  OR ([dbo].[Aircraft].[ShipNbr] LIKE @AircraftTextMatch))
    ORDER BY [Aircraft].[MSN];
    DECLARE @FromID AS INT;
    DECLARE @ToID AS INT;
    IF ((@Start = -1)
        OR (@Count = -1))
        BEGIN
            SELECT @FromID = 0;
            SELECT @ToID = 999999;
        END
    ELSE
        BEGIN
            SELECT @FromID = @Start;
            SELECT @ToID = @Start + @Count - 1;
        END
    CREATE TABLE #tempFlightUsage (
        #tempFlightUsageId INT            IDENTITY PRIMARY KEY,
        AircraftId          INT           ,
        CurrentUsage     DECIMAL (8, 1)
    );
    INSERT INTO #tempFlightUsage (AircraftId, CurrentUsage)
    SELECT   AircraftId,
             SUM(FlightUsage)
    FROM     (SELECT Flight.AircraftId,
                     (HobbsEnd - HobbsStart) AS FlightUsage
              FROM   dbo.Flight
                     INNER JOIN
                     #temp
                     ON #temp.AircraftId = dbo.Flight.AircraftId
              WHERE  dbo.flight.IsActive = 1
                     AND dbo.flight.IsDeleted = 0) AS T
    GROUP BY T.AircraftId;
    SELECT   [dbo].Aircraft.AircraftId,
             IsNull([dbo].Location.LocationName, 'Not Assigned') AS CurrentLocationName,
             [dbo].Model.StringValue AS ModelName,
             [dbo].Aircraft.MSN,
             [dbo].Aircraft.ShipNbr,
             [dbo].Aircraft.SerialNbr,
             [dbo].Aircraft.RegistrationNbr,
             [dbo].Aircraft.Description,
             [dbo].Aircraft.CurrentLocationId,
             [dbo].Aircraft.LiveryColor,
             [dbo].aircraft.AirframeHoursStart,
             [dbo].Aircraft.AirframeHoursCurrent,
             [dbo].Aircraft.AirframeHoursOffset,
             [dbo].Aircraft.MeterUnitId,
             dbo.Aircraft.TotalRequiredUsage,
             ISNULL([#tempFlightUsage].CurrentUsage, 0) AS CurrentUsage,
             [dbo].Aircraft.EstEndOfTesting,
             [dbo].Aircraft.ActEndOfTesting,
             [dbo].AircraftStatusDef.StringValue AS AircraftStatus,
             [dbo].Aircraft.AircraftEngineHoursCurrent,
             [dbo].Aircraft.IsActive,
             [dbo].Aircraft.IsDeleted,
             [dbo].Aircraft.UTCDateCreated,
             [dbo].Aircraft.UTCDateModified
    FROM     [dbo].[Aircraft]
             LEFT OUTER JOIN
             dbo.Location
             ON Location.LocactionId = Aircraft.CurrentLocationId
             INNER JOIN
             dbo.Model
             ON Model.ID = Aircraft.ModelId
             INNER JOIN
             dbo.AircraftStatusDef
             ON dbo.AircraftStatusDef.Id = Aircraft.AircraftStatusId
             INNER JOIN
             #temp
             ON [dbo].[Aircraft].[AircraftId] = #temp.[AircraftId]
             LEFT OUTER JOIN
             #tempFlightUsage
             ON #tempFlightUsage.AircraftId = dbo.Aircraft.AircraftId
    WHERE    #temp.tempID >= @FromID
             AND #temp.tempID <= @ToID
    ORDER BY dbo.Aircraft.MSN;
    SELECT @Total = COUNT(*)
    FROM   #temp;
    DROP TABLE #temp;
    DROP TABLE #tempFlightUsage;
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftInfosForTest]
	@TestId INT,
	@ProgId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].Aircraft.AircraftId,
		[dbo].Location.LocationName AS CurrentLocationName,
		[dbo].Model.StringValue AS ModelName,
		[dbo].Aircraft.MSN,
		[dbo].Aircraft.ShipNbr,
		[dbo].Aircraft.SerialNbr,
		[dbo].Aircraft.RegistrationNbr,
		[dbo].Aircraft.Description,
		[dbo].Aircraft.LiveryColor,
		[dbo].AircraftStatusDef.StringValue AS AircraftStatus,
		[dbo].Aircraft.IsActive,
		[dbo].Aircraft.IsDeleted
	FROM Aircraft 
		INNER JOIN AircraftTest 
			ON AircraftTestXref.AircraftId = Aircraft.Id
		INNER JOIN Test
			ON Test.Id = AircraftTestXref.Id
		INNER JOIN Location
			ON Location.LocactionId = Aircraft.CurrentLocationId
		INNER JOIN Model	
			ON Model.Id = Aircraft.ModelId
	WHERE 
		Test.Id = @TestId
		AND Test.ProgId = @ProgId
		AND Test.IsActive = 1
		AND AircraftTestXref.IsDeleted = 0
		AND Aircraft.ProgId = @ProgId
		AND Aircraft.IsActive = 1
		AND Aircraft.IsDeleted = 0
	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftInfosNotForTest]
	@TestId INT,
	@ProgId INT
AS
BEGIN
	SET NOCOUNT ON

	SELECT 
		[dbo].Aircraft.AircraftId,
		[dbo].Location.LocationName AS CurrentLocationName,
		[dbo].Model.StringValue AS ModelName,
		[dbo].Aircraft.MSN,
		[dbo].Aircraft.ShipNbr,
		[dbo].Aircraft.SerialNbr,
		[dbo].Aircraft.RegistrationNbr,
		[dbo].Aircraft.Description,
		[dbo].Aircraft.LiveryColor,
		[dbo].AircraftStatusDef.StringValue AS AircraftStatus,
		[dbo].Aircraft.IsActive,
		[dbo].Aircraft.IsDeleted
	FROM Aircraft 
		INNER JOIN Location
			ON Location.LocactionId = Aircraft.CurrentLocationId
		INNER JOIN Model	
			ON Model.Id = Aircraft.ModelId
	WHERE 
		Aircraft.ProgId = @ProgId
		AND Aircraft.IsActive = 1
		AND Aircraft.IsDeleted = 0
		AND Aircraft.AircraftId NOT IN (
			SELECT 
				[dbo].Aircraft.AircraftId
			FROM Aircraft 
				INNER JOIN AircraftTest 
					ON AircraftTestXref.AircraftId = Aircraft.Id
				INNER JOIN Test
					ON Test.Id = AircraftTestXref.Id
				INNER JOIN Location
					ON Location.LocactionId = Aircraft.LocationId
				INNER JOIN Model	
					ON Model.Id = Aircraft.ModelId
			WHERE 
				Test.Id = @TestId
				AND Test.ProgId = @ProgId
				AND Test.IsActive = 1
				AND AircraftTestXref.IsDeleted = 0
				AND Aircraft.ProgId = @ProgId
				AND Aircraft.IsActive = 1
				AND Aircraft.IsDeleted = 0 )
	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftLocations]
	@AircraftId Int = 0,
	@LocationId Int = 0,
	@IsActiveFilter int = 1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[AircraftLocationId] [Int],
		[AircraftId]  [Int],
		[LocationId]  [Int],
		[ArrivalDate]  [Datetime],
		[ActEndOfTest]  [DateTime]
	)

	INSERT INTO #temp (
		[AircraftLocationId],
        [AircraftId],
        [LocationId],
		[ArrivalDate],
		[ActEndOfTest]
	)
	SELECT
		[AircraftLocationId],
        [AircraftId],
        [LocationId],
		IsNull([ArrivalDate], Convert(DateTime, '1-Jan-1900')) As ArrivalDate,
		IsNull([ActEndOfTest], Convert(Datetime, '12-Dec-2099')) AS ActEndOfTest
	FROM [dbo].[AircraftLocation]
	WHERE
		((@IsActiveFilter = 1 AND [AircraftLocation].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AircraftLocation].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [AircraftLocation].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [AircraftLocation].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@AircraftId = 0) OR ([dbo].[AircraftLocation].[AircraftId] = @AircraftId)) AND 
		((@LocationId = 0) OR ([dbo].[AircraftLocation].[LocationId] = @LocationId))
	ORDER BY AircraftLocation.ArrivalDate DESC

	SELECT @Total = COUNT(*) FROM #temp

	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END
	
	Delete #temp
	Where tempId < @FromId Or tempId > @ToId

	CREATE TABLE #tempFlightUsage (
		#tempFlightUsageId INT IDENTITY PRIMARY KEY,
		AircraftId INT,
		LocationId INT,
		CurrentUsage DECIMAL(12,1)
	)
	
	INSERT INTO #tempFlightUsage (
		AircraftId,
		LocationId,
		CurrentUsage
		)
	SELECT
		T.AircraftId,
		T.LocationId,
		SUM(T.FlightUsage)
	FROM
		(SELECT
			Flight.AircraftId,
			Flight.LocationId,
			FlightUsage = (Flight.HobbsEnd - Flight.HobbsStart)
		FROM
			dbo.Flight
			INNER JOIN #temp ON #temp.AircraftId = Flight.AircraftId AND #temp.LocationId = Flight.LocationId
		WHERE
			dbo.flight.DepartureDateTime_Local >= #temp.ArrivalDate AND
			dbo.flight.DepartureDateTime_Local <= #temp.ActEndOfTest AND
			dbo.flight.IsActive = 1 AND
			dbo.flight.IsDeleted = 0) AS T
	GROUP BY 
		T.AircraftId,
		T.LocationId
	DECLARE @LocationCount INT

	SELECT 
		[dbo].[AircraftLocation].AircraftLocationid,
		[dbo].aircraftLocation.AircraftId,
		dbo.AircraftLocation.LocationId,
		dbo.AircraftLocation.ArrivalDate,
		dbo.AircraftLocation.RequiredUsage,
		CONVERT(DECIMAL(9,1),ISNULL(#tempFlightUsage.CurrentUsage,0)) AS AccumUsage,
		dbo.AircraftLocation.EstEndOfTest,
		dbo.AircraftLocation.ActEndOfTest,
		dbo.AircraftLocation.LastLocation,
		dbo.AircraftLocation.AircraftLocationGUID,
		dbo.AircraftLocation.IsActive,
		dbo.AircraftLocation.IsDeleted,
		dbo.AircraftLocation.UTCDateCreated,
		dbo.AircraftLocation.UTCDateModified,
		dbo.AircraftLocation.StampUserName,
		dbo.AircraftLocation.ConcurrencyId,
		[dbo].Aircraft.SerialNbr,
		[dbo].Aircraft.RegistrationNbr,
		[dbo].Aircraft.MSN,
		[dbo].Aircraft.ShipNbr,
		[dbo].Location.LocationName
	FROM [dbo].[AircraftLocation]
		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[AircraftLocation].[AircraftId]
		INNER JOIN [dbo].[Location] ON
			[dbo].[Location].[LocactionId] = [dbo].[AircraftLocation].[LocationId]
		INNER JOIN #temp ON 
			[dbo].[AircraftLocation].[AircraftLocationId] = #temp.[AircraftLocationId] 
		LEFT OUTER JOIN #tempFlightUsage ON 
			dbo.aircraftLocation.aircraftId = #tempFlightUsage.AircraftId AND dbo.AircraftLocation.LocationId = #tempFlightUsage.LocationId
	ORDER BY 
		#temp.tempID

	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listFleet]
	@ProgId Int = 0,
	@CurrentLocationId INT = 0,
	@ModelId Int = 0,
	@EngineerId Int = 0,
	@MechanicId Int = 0,
	@AircraftStatusId Int = 0,
	@ManufacturerId Int = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[AircraftId] [Int]
	)

	INSERT INTO #temp (
		[AircraftId]
	)
	SELECT
		[AircraftId]
	FROM [dbo].[Aircraft]
	WHERE
		((@IsActiveFilter = 1 AND [Aircraft].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [Aircraft].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [Aircraft].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [Aircraft].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[Aircraft].[ProgId] = @ProgId)) AND 
		((@CurrentLocationId = 0) OR ([dbo].[Aircraft].[CurrentLocationId] = @CurrentLocationId)) AND 
		((@ModelId = 0) OR ([dbo].[Aircraft].[ModelId] = @ModelId)) AND 
		((@EngineerId = 0) OR ([dbo].[Aircraft].[EngineerId] = @EngineerId)) AND 
		((@MechanicId = 0) OR ([dbo].[Aircraft].[MechanicId] = @MechanicId)) AND 
		((@AircraftStatusId = 0) OR ([dbo].[Aircraft].[AircraftStatusId] = @AircraftStatusId)) AND 
		((@ManufacturerId = 0) OR ([dbo].[Aircraft].[ManufacturerId] = @ManufacturerId))
	ORDER BY
		[Aircraft].[MSN]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[Aircraft].* FROM [dbo].[Aircraft]
	INNER JOIN #temp ON 
		[dbo].[Aircraft].[AircraftId] = #temp.[AircraftId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listAircraftStatusDefs]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[AircraftStatusDef]
	WHERE
		((@IsActiveFilter = 1 AND [AircraftStatusDef].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AircraftStatusDef].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[AircraftStatusDef].[ProgId] = @ProgId))
	ORDER BY
		[AircraftStatusDef].[SortOrder],
		[AircraftStatusDef].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[AircraftStatusDef].* FROM [dbo].[AircraftStatusDef]
	INNER JOIN #temp ON 
		[dbo].[AircraftStatusDef].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[AircraftStatusDef]
	WHERE
		((@IsActiveFilter = 1 AND [AircraftStatusDef].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [AircraftStatusDef].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[AircraftStatusDef].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[listAircraftStatuses]
	@ProgId Int = 0,
	@SortBy varchar(32)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		v.AircraftId,
		IsNull(vs.AircraftStatusId, 0) as AircraftStatusId,
		IsNull(loc.LocationName, '') as LocationName, 
		v.MSN, 
		IsNull(vs.Status, '') as [Status], 
		IsNull(vs.EngineerComment, '') as EngineerComment, 
		IsNull(vs.HangarComment, '') as HangarComment, 
		vs.UTCDateModified 
	INTO
		#temp
	FROM 
		Aircraft v
		left outer join AircraftStatus vs 
			ON v.AircraftId = vs.AircraftId
		left outer join Location loc
			ON v.CurrentLocationId = loc.LocactionId
	WHERE 
		v.IsDeleted = 0 AND 
		((@ProgId = 0) OR (v.ProgId = @ProgId))

	if @SortBy = 'Location' 
	begin
		SELECT * FROM #temp
		ORDER BY LocationName DESC, MSN
	end 

	if @SortBy = 'MSN' 
	begin
		SELECT * FROM #temp
		ORDER BY MSN
	end 

	if @SortBy = 'Date' 
	begin
		SELECT * FROM #temp
		ORDER BY UTCDateModified DESC, MSN
	end 
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWeightDefs]
	@ProgId INT = 0,
	@WeightDefTypeDescr NVARCHAR(50) = '',
	@WeightDefTypeId INT = 0,
	@IsActiveFilter int = -1, 
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WeightDefId] [Int]
	)

	INSERT INTO #temp (
		[WeightDefId]
	)
	SELECT
		[WeightDefId]
	FROM [dbo].[WeightDef]
		INNER JOIN WeightUnit ON WeightUnit.ID = WeightDef.WeightUnitId
		LEFT OUTER JOIN WeightDefType ON WeightDefType.WeightDefTypeID = WeightDef.WeightDefTypeId
	WHERE
		((@IsActiveFilter = 1 AND [WeightDef].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WeightDef].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@IsDeletedFilter = 1 AND [WeightDef].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WeightDef].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WeightDef].[ProgId] = @ProgId)) AND 
		((@WeightDefTypeDescr = '') OR (lower(WeightDefType.WeightDefTypeDescr) = lower(@WeightDefTypeDescr))) AND
		((@WeightDefTypeId = 0) OR ([dbo].[WeightDef].[WeightDefTypeId] = @WeightDefTypeId))  
	ORDER BY
		[WeightDef].[SortOrder],
		[WeightDef].[WeightDefDescr]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[WeightDef].*,
		ISNULL(WeightDefType.WeightDefTypeDescr, 'N/A') AS WeightDefTypeDescr,
		WeightUnit.StringValue AS WeightUnit
	FROM [dbo].[WeightDef]
	INNER JOIN #temp ON 
		[dbo].[WeightDef].[WeightDefId] = #temp.[WeightDefId]
	INNER JOIN WeightUnit ON
		WeightUnit.Id = WeightDef.WeightUnitId
	LEFT OUTER JOIN WeightDefType ON
		WeightDefType.WeightDefTypeId = WeightDef.WeightDefTypeID
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWeightDefTypes]
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WeightDefTypeId] [Int]
	)

	INSERT INTO #temp (
		[WeightDefTypeId]
	)
	SELECT
		[WeightDefTypeId]
	FROM [dbo].[WeightDefType]
	ORDER BY
		[WeightDefType].[SortOrder],
		[WeightDefType].[WeightDefTypeDescr]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WeightDefType].* FROM [dbo].[WeightDefType]
	INNER JOIN #temp ON 
		[dbo].[WeightDefType].[WeightDefTypeId] = #temp.[WeightDefTypeId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWeightUnits]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[WeightUnit]
	WHERE
		((@IsActiveFilter = 1 AND [WeightUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WeightUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WeightUnit].[ProgId] = @ProgId))
	ORDER BY
		[WeightUnit].[StringValue],
		[WeightUnit].[SortOrder]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WeightUnit].* FROM [dbo].[WeightUnit]
	INNER JOIN #temp ON 
		[dbo].[WeightUnit].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[WeightUnit]
	WHERE
		((@IsActiveFilter = 1 AND [WeightUnit].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WeightUnit].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WeightUnit].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWOEngineerComments]
	@WorkOrderId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[EngineerCommentsId] [Int]
	)

	INSERT INTO #temp (
		[EngineerCommentsId]
	)
	SELECT
		[EngineerCommentsId]
	FROM [dbo].[WOEngineerComment]
	WHERE
		((@IsDeletedFilter = 1 AND [WOEngineerComment].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WOEngineerComment].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WOEngineerComment].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[WOEngineerComment].*,
		AircraftComponent.StringValue AS Component,
		ISNULL(IssueCode.StringValue, '') AS IssueCode
	FROM [dbo].[WOEngineerComment]
	INNER JOIN #temp ON 
		[dbo].[WOEngineerComment].[EngineerCommentsId] = #temp.[EngineerCommentsId]
	INNER JOIN AircraftComponent ON
		dbo.AircraftComponent.ID = dbo.WOEngineerComment.ComponentId
	LEFT OUTER JOIN IssueCode ON 
		IssueCode.Id = dbo.WOEngineerComment.IssueCodeId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWOInstructions]
	@WorkOrderId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WOInstructionsId] [Int]
	)

	INSERT INTO #temp (
		[WOInstructionsId]
	)
	SELECT
		[WOInstructionsId]
	FROM [dbo].[WOInstruction]
	WHERE
		((@IsDeletedFilter = 1 AND [WOInstruction].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WOInstruction].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WOInstruction].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WOInstruction].* FROM [dbo].[WOInstruction]
	INNER JOIN #temp ON 
		[dbo].[WOInstruction].[WOInstructionsId] = #temp.[WOInstructionsId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWOPerformedWorks]
	@WorkOrderId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[PerformedWorkId] [Int]
	)

	INSERT INTO #temp (
		[PerformedWorkId]
	)
	SELECT
		[PerformedWorkId]
	FROM [dbo].[WOPerformedWork]
	WHERE
		((@IsDeletedFilter = 1 AND [WOPerformedWork].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WOPerformedWork].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WOPerformedWork].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WOPerformedWork].* FROM [dbo].[WOPerformedWork]
	INNER JOIN #temp ON 
		[dbo].[WOPerformedWork].[PerformedWorkId] = #temp.[PerformedWorkId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[listWOReasons]
	@WorkOrderId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WOReasonId] [Int]
	)

	INSERT INTO #temp (
		[WOReasonId]
	)
	SELECT
		[WOReasonId]
	FROM [dbo].[WOReason]
	WHERE
		((@IsDeletedFilter = 1 AND [WOReason].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WOReason].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WOReason].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WOReason].* FROM [dbo].[WOReason]
	INNER JOIN #temp ON 
		[dbo].[WOReason].[WOReasonId] = #temp.[WOReasonId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderForDocument]
	@DocId Int
AS
BEGIN
	SET NOCOUNT ON

	
	SELECT L.* FROM [dbo].[WorkOrder] AS L
	INNER JOIN [dbo].[WorkOrderDocXref] AS X
		ON X.[WorkOrderId] = L.[WorkOrderId]
	INNER JOIN [dbo].[Document] AS R
		ON R.[DocId] = X.[DocumentId]
	WHERE 
		R.[DocId] = @DocId
	

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderInfos]
	@ProgId INT,
	@AircraftId INT = 0,
	@AircraftTextMatch NVARCHAR(30) = '',
	@LocationId INT = 0,
	@MechanicId Int = 0,
	@WorkOrderNbr nvarchar(20) = '',
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WorkOrderId] [Int]
	)

	INSERT INTO #temp (
		[WorkOrderId]
	)
	SELECT
		[WorkOrderId]
	FROM 
		[dbo].[WorkOrder]
		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[WorkOrder].[AircraftId]		
	WHERE
		((@IsDeletedFilter = 1 AND [WorkOrder].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WorkOrder].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		(WorkOrder.ProgId = @ProgID) AND
		((@AircraftId = 0) OR ([dbo].[WorkOrder].[AircraftId] = @AircraftId)) AND 
		((@AircraftTextMatch = '') OR 
		 ([dbo].[Aircraft].[SerialNbr] LIKE @AircraftTextMatch) OR
		 ([dbo].[Aircraft].[RegistrationNbr] LIKE @AircraftTextMatch) OR 
		 ([dbo].[Aircraft].[MSN] LIKE @AircraftTextMatch) OR 
		 ([dbo].[Aircraft].[ShipNbr] LIKE @AircraftTextMatch)) AND
		((@LocationId = 0) OR ([dbo].[WorkOrder].[LocationId] = @LocationId)) AND 
		((@MechanicId = 0) OR ([dbo].[WorkOrder].[MechanicId] = @MechanicId)) AND 
		((@WorkOrderNbr = '') OR ([dbo].[WorkOrder].[WorkOrderNbr] LIKE @WorkOrderNbr))
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[WorkOrder].[WorkOrderId],
		[dbo].[WorkOrder].[AircraftId],
		[dbo].[Aircraft].[MSN],
		[dbo].[Aircraft].[ShipNbr],
		[dbo].[Aircraft].[SerialNbr],
		[dbo].[Aircraft].[RegistrationNbr],
		[dbo].[WorkOrder].[LocationId],
		[dbo].[Location].[LocationName],
		[dbo].[WorkOrder].[WorkOrderNbr],
		[dbo].[WorkOrder].[FISComplaintNbr],
		[dbo].[WorkOrder].[SquawkNbr],
		[dbo].[WorkOrder].[RequestDate],
		[dbo].[WorkOrder].[DueDate],
		[dbo].[WorkOrder].[FinishDate],
		[dbo].[WorkOrder].[WorkOrderStatusId],
		[dbo].[WorkOrderStatus].[StringValue] AS WorkOrderStatus,
		[dbo].[WorkOrder].[IsDeleted]
	FROM 
		[dbo].[WorkOrder]
		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[WorkOrder].[AircraftId]
		INNER JOIN [dbo].[Location] ON
			[dbo].[Location].[LocactionId] = [dbo].[WorkOrder].[LocationId]
		INNER JOIN [dbo].WorkOrderStatus ON
			[dbo].WorkOrder.WorkOrderStatusId = dbo.WorkOrderStatus.ID
		INNER JOIN #temp ON 
			[dbo].[WorkOrder].[WorkOrderId] = #temp.[WorkOrderId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderInfosForDocument]
	@DocumentId Int
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		[dbo].[WorkOrder].[WorkOrderId],
		[dbo].[WorkOrder].[AircraftId],
		[dbo].[Aircraft].[MSN],
		[dbo].[Aircraft].[ShipNbr],
		[dbo].[Aircraft].[SerialNbr],
		[dbo].[Aircraft].[RegistrationNbr],
		[dbo].[WorkOrder].[LocationId],
		[dbo].[Location].[LocationName],
		[dbo].[WorkOrder].[WorkOrderNbr],
		[dbo].[WorkOrder].[FISComplaintNbr],
		[dbo].[WorkOrder].[SquawkNbr],
		[dbo].[WorkOrder].[RequestDate],
		[dbo].[WorkOrder].[DueDate],
		[dbo].[WorkOrder].[FinishDate],
		[dbo].[WorkOrder].[WorkOrderStatusId],
		[dbo].[WorkOrderStatus].[StringValue] AS WorkOrderStatus,
		[dbo].[WorkOrder].IsDeleted
	FROM 
		[dbo].[WorkOrder]

		INNER JOIN [dbo].[Aircraft] ON
			[dbo].[Aircraft].[AircraftId] = [dbo].[WorkOrder].[AircraftId]
		INNER JOIN [dbo].[Location] ON
			[dbo].[Location].[LocactionId] = [dbo].[WorkOrder].[LocationId]
		INNER JOIN [dbo].WorkOrderStatus ON
			[dbo].WorkOrder.WorkOrderStatusId = dbo.WorkOrderStatus.ID
		INNER JOIN [dbo].[WorkOrderDocXref] ON
			dbo.WorkOrderDocXref.[WorkOrderId] = dbo.WorkOrder.[WorkOrderId]
		INNER JOIN [dbo].[DocumentInfo] ON
			DocumentInfo.[DocumentId] = dbo.WorkOrderDocXref.[DocumentId]
	WHERE 
		dbo.DocumentInfo.[DocumentId] = @DocumentId	AND
		dbo.WorkOrder.IsDeleted = 0 AND
		dbo.WorkOrderDocXref.IsDeleted = 0 AND
		dbo.DocumentInfo.IsDeleted = 0

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderParts]
	@WorkOrderId Int,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WorkOrderPartId] [Int]
	)

	INSERT INTO #temp (
		[WorkOrderPartId]
	)
	SELECT
		[WorkOrderPartId]
	FROM [dbo].[WorkOrderPart]
	WHERE
		((@IsDeletedFilter = 1 AND [WorkOrderPart].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WorkOrderPart].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WorkOrderPart].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WorkOrderPart].* FROM [dbo].[WorkOrderPart]
	INNER JOIN #temp ON 
		[dbo].[WorkOrderPart].[WorkOrderPartId] = #temp.[WorkOrderPartId]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderStatuss]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[WorkOrderStatus]
	WHERE
		((@IsActiveFilter = 1 AND [WorkOrderStatus].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkOrderStatus].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkOrderStatus].[ProgId] = @ProgId))
	ORDER BY
		[WorkOrderStatus].[SortOrder],
		[WorkOrderStatus].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WorkOrderStatus].* FROM [dbo].[WorkOrderStatus]
	INNER JOIN #temp ON 
		[dbo].[WorkOrderStatus].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[WorkOrderStatus]
	WHERE
		((@IsActiveFilter = 1 AND [WorkOrderStatus].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkOrderStatus].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkOrderStatus].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkOrderTypes]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[WorkOrderType]
	WHERE
		((@IsActiveFilter = 1 AND [WorkOrderType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkOrderType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkOrderType].[ProgId] = @ProgId))
	ORDER BY
		[WorkOrderType].[SortOrder],
		[WorkOrderType].[StringValue]
	
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WorkOrderType].* FROM [dbo].[WorkOrderType]
	INNER JOIN #temp ON 
		[dbo].[WorkOrderType].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[WorkOrderType]
	WHERE
		((@IsActiveFilter = 1 AND [WorkOrderType].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkOrderType].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkOrderType].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWorkShifts]
	@ProgId Int = 0,
	@IsActiveFilter int = -1, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[Id] [Int]
	)

	INSERT INTO #temp (
		[Id]
	)
	SELECT
		[Id]
	FROM [dbo].[WorkShift]
	WHERE
		((@IsActiveFilter = 1 AND [WorkShift].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkShift].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkShift].[ProgId] = @ProgId))
	ORDER BY
		[WorkShift].[SortOrder],
		[WorkShift].[StringValue]

	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT [dbo].[WorkShift].* FROM [dbo].[WorkShift]
	INNER JOIN #temp ON 
		[dbo].[WorkShift].[Id] = #temp.[Id]
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	DROP TABLE #temp
	SELECT @Total = COUNT(*) FROM [dbo].[WorkShift]
	WHERE
		((@IsActiveFilter = 1 AND [WorkShift].[IsActive] = 1) OR (@IsActiveFilter = 0 AND [WorkShift].[IsActive] = 0) OR (@IsActiveFilter = -1)) AND 
		((@ProgId = 0) OR ([dbo].[WorkShift].[ProgId] = @ProgId))

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[listWOMechanicComments]
	@WorkOrderId Int = 0,
	@IsDeletedFilter int = 0, 
	@Start int = -1,
	@Count int = -1,
	@Total int OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		[WOMechanicCommentsId] [Int]
	)

	INSERT INTO #temp (
		[WOMechanicCommentsId]
	)
	SELECT
		[WOMechanicCommentsId]
	FROM [dbo].[WOMechanicComment]
	WHERE
		((@IsDeletedFilter = 1 AND [WOMechanicComment].[IsDeleted] = 1) OR (@IsDeletedFilter = 0 AND [WOMechanicComment].[IsDeleted] = 0) OR (@IsDeletedFilter = -1)) AND 
		([dbo].[WOMechanicComment].[WorkOrderId] = @WorkOrderId)
	DECLARE @FromID INT
	DECLARE @ToID INT

	IF ((@Start = -1) OR (@Count = -1))
		BEGIN
		SELECT @FromID  = 0
		SELECT @ToID    = 999999
		END
	ELSE
		BEGIN
		SELECT @FromID  = @Start
		SELECT @ToID    = @Start + @Count - 1
		END

	SELECT 
		[dbo].[WOMechanicComment].*,
		AircraftComponent.StringValue AS Component,
		ISNULL(IssueCode.StringValue, '') AS IssueCode
	FROM [dbo].[WOMechanicComment]
	INNER JOIN #temp ON 
		[dbo].[WOMechanicComment].[WOMechanicCommentsId] = #temp.[WOMechanicCommentsId]
	INNER JOIN AircraftComponent ON
		AircraftComponent.ID = dbo.WOMechanicComment.ComponentId
	LEFT OUTER JOIN IssueCode ON
		IssueCode.Id = dbo.WOMechanicComment.IssueCodeId
	WHERE #temp.tempID >= @FromID AND #temp.tempID <= @ToID 
	ORDER BY #temp.tempID

	SELECT @Total = COUNT(*) FROM #temp
	DROP TABLE #temp

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptDailyUsage]
	@ProgId INT,
	@LocationId INT = -1,
	@FlightDate DATETIME
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #FleetAtLocationByDate (
		ID INTEGER IDENTITY PRIMARY KEY,
		AircraftLocationId INTEGER,
		AircraftId INTEGER,
		LocationId INTEGER,
		LocationGoalHours DECIMAL(9,1),
		EndTestDate DATETIME
		)
		
	INSERT INTO #FleetAtLocationByDate (
		AircraftLocationId,
		AircraftId,
		LocationId,
		LocationGoalHours,
		EndTestDate
		)
	SELECT 
		VL.AircraftLocationId,
		V.AircraftID,
		L.LocactionId,
		VL.RequiredUsage AS LocationGoalHours,
		CASE 
			WHEN (VL.ActEndOfTest <> NULL) THEN
				VL.ActEndOfTest
			WHEN (VL.EstEndOfTest <> NULL) THEN
				VL.EstEndOfTest
			ELSE
				NULL
		END AS EndTestDate
	FROM
		[dbo].[AircraftLocation] AS VL
		INNER JOIN [dbo].[Aircraft] AS V ON
			V.AircraftId = VL.AircraftId
		INNER JOIN [dbo].Location AS L ON
			L.LocactionId = VL.LocationId
	WHERE
		V.ProgId = @ProgId AND
		V.IsDeleted = 0 AND	V.IsActive = 1 AND
		((@LocationId = -1) OR (L.LocactionId = @LocationId)) AND
		((NOT (VL.ActEndOfTest IS NULL) AND (VL.ActEndOfTest >= @FlightDate)) OR
		 ((VL.ActEndOfTest IS NULL) AND NOT (VL.EstEndOfTest = NULL) AND (VL.EstEndOfTest >= @FlightDate)) OR
		 ((VL.ActEndOfTest IS NULL) AND (VL.EstEndOfTest IS NULL)))
		
	CREATE TABLE #FleetAtLocationOnDate (
		ID INTEGER IDENTITY PRIMARY KEY,
		AircraftLocationId INTEGER,
		AircraftId INTEGER,
		LocationId INTEGER,
		LocationGoalHours DECIMAL(9,1) NULL,
		EndTestDate DATETIME NULL
		)
	
	INSERT INTO #FleetAtLocationOnDate (
		AircraftLocationId,
		AircraftId,
		LocationId,
		LocationGoalHours,
		EndTestDate
	) 
	SELECT DISTINCT
		A.AircraftLocationId,
		A.AircraftId,
		A.LocationId,
		A.LocationGoalHours,
		A.EndTestDate
	FROM
		#FleetAtLocationByDate AS A
		INNER JOIN
			(SELECT
				AircraftId, 
				MIN(EndTestDate) AS EndTestDate
			 FROM
				#FleetAtLocationByDate
			 GROUP BY
				AircraftId) AS B ON
			A.AircraftId = B.AircraftId AND ((A.EndTestDate = B.EndTestDate) OR (A.EndTestDate IS NULL AND B.EndTestDate IS NULL))

	CREATE TABLE #AircraftLocationHoursOnDate (
		ID INTEGER IDENTITY PRIMARY KEY,
		AircraftId INTEGER,
		LocationId INTEGER,
		DateLastRun DateTime,
		CurrentMeter DECIMAL(9,1),
		TotalFlightUsage DECIMAL(9,1)
	)

	INSERT INTO #AircraftLocationHoursOnDate (
		AircraftId,
		LocationId,
		DateLastRun,
		CurrentMeter,
		TotalFlightUsage
	)
	SELECT
		Z.AircraftId,
		Z.LocationId,
		MAX(Z.FlightDateSort) AS DateLastRun,
		MAX(Z.AirframeHoursEnd) AS CurrentMeter,
		SUM(Z.FlightUsage) AS TotalFlightUsage
	FROM
		(SELECT
			T.AircraftId,
			T.LocationId,
			T.FlightDateSort,
			T.AirframeHoursEnd,
			FlightUsage = (T.AirframeHoursEnd - T.AirframeHoursStart)
		 FROM
			Flight AS T
			INNER JOIN #FleetAtLocationByDate AS A ON
				A.AircraftId = T.AircraftId AND A.LocationId = T.LocationId
		 WHERE
			Convert(DateTime, T.FlightDate) <= @FlightDate AND
			T.IsDeleted = 0 AND T.IsActive = 1) AS Z
	GROUP BY
		Z.AircraftId,
		Z.LocationId

	CREATE TABLE #AircraftTotalHoursOnDate (
		ID INTEGER IDENTITY PRIMARY KEY,
		AircraftId INTEGER,
		TotalAircraftUsage DECIMAL(9,1)
	)

	INSERT INTO #AircraftTotalHoursOnDate (
		AircraftId,
		TotalAircraftUsage
	)
	SELECT
		Z.AircraftId,
		SUM(Z.FlightUsage) AS TotalFlightUsage
	FROM
		(SELECT
			T.AircraftId,
			FlightUsage = (T.AirframeHoursEnd - T.AirframeHoursStart)
		 FROM
			Flight AS T
			INNER JOIN #FleetAtLocationByDate AS A ON
				A.AircraftId = T.AircraftId AND A.LocationId = T.LocationId
		 WHERE
			Convert(DateTime, T.FlightDate) <= @FlightDate AND
			T.IsDeleted = 0 AND T.IsActive = 1) AS Z
	GROUP BY
		Z.AircraftId

	SELECT
		Program.Progid,
		Program.ProgName,
		A.AircraftLocationId,
		A.LocationId,
		Location.LocationName,
		@FlightDate AS FlightDate,
		A.AircraftId,
		Aircraft.MSN,
		Model.StringValue AS ModelName,
		ISNULL(Test.StringValue, 'None Assigned') AS TestName,
		ISNULL(B.FlightId, 0) AS FlightId,
		ISNULL(B.AirframeHoursEnd, 0) - ISNULL(B.AirframeHoursStart, 0) AS HoursOnDate,
		C.DateLastRun,
		ISNULL(C.CurrentMeter, Aircraft.AirframeHoursStart) AS LastMeterReading,
		C.TotalFlightUsage AS LocationCurrentHours,
		CASE ISNULL(A.LocationGoalHours, 0)
			WHEN 0 THEN
				0
			ELSE
				CONVERT(FLOAT, ((100 * ISNULL(C.TotalFlightUsage, 0)) / A.LocationGoalHours))
			END AS LocationPercentComplete,
		A.LocationGoalHours,
		A.EndTestDate AS LocationEndTestDate,
		D.TotalAircraftUsage AS AircraftCurrentUsage,
		CASE ISNULL(Aircraft.TotalRequiredUsage, 0)
			WHEN 0 THEN
				0
			ELSE
				CONVERT(FLOAT, ((100 * ISNULL(D.TotalAircraftUsage, 0)) / Aircraft.TotalRequiredUsage))
			END AS ProgramPercentComplete,
		CASE
			WHEN NOT(Aircraft.EstEndOfTesting IS NULL) THEN
				Aircraft.EstEndOfTesting
			ELSE
				NULL
		END AS ProgramEndTestDate,
		Aircraft.TotalRequiredUsage
	FROM
		#FleetAtLocationOnDate AS A
		INNER JOIN Aircraft ON Aircraft.AircraftId = A.AircraftId
		INNER JOIN Program ON Program.ProgId = Aircraft.ProgId
		INNER JOIN Model ON Model.ID = Aircraft.ModelId
		LEFT OUTER JOIN AircraftTestXref ON AircraftTestXRef.AircraftId = Aircraft.AircraftId
		LEFT OUTER JOIN Test ON Test.ID = AircraftTestXref.TestId
		INNER JOIN Location ON Location.LocactionId = A.LocationId
		LEFT OUTER JOIN Flight AS B ON B.AircraftId = A.AircraftId AND B.LocationId = A.LocationId AND CONVERT(DATETIME, b.FlightDate) = @FlightDate
		LEFT OUTER JOIN #AircraftLocationHoursOnDate AS C ON C.AircraftId = A.AircraftId AND C.LocationId = A.LocationId
		LEFT OUTER JOIN #AircraftTotalHoursOnDate AS D ON D.AircraftId = A.AircraftId
	WHERE
		Program.IsDeleted = 0 AND Program.IsActive = 1 AND
		Aircraft.IsDeleted = 0 AND Aircraft.IsActive = 1 AND
		ISNULL(Test.IsActive, 1) = 1 AND
		ISNULL(B.IsDeleted, 0) = 0 AND ISNULL(B.IsActive, 1) = 1
	ORDER BY
		Program.ProgName,
		Program.ProgId,
		Aircraft.MSN,
		Aircraft.AircraftId
		
	DROP TABLE #FleetAtLocationOnDate
	DROP TABLE #FleetAtLocationByDate
	DROP TABLE #AircraftLocationHoursOnDate
	
END

SET ANSI_NULLS ON
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptPilotComments]
	@ProgId INT,
	@LocationId INT = -1,
	@AircraftId INT = -1,
	@DateFrom DATETIME,
	@DateThru DATETIME
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		P.ProgName,
		L.LocationName,
		L.LocactionId AS LocationId,
		V.MSN,
		V.AircraftId,
		T.FlightDateSort,
		T.FlightDate,
		T.FlightId,
		T.AirframeHoursEnd,
		D.StringValue AS PilotName,
		VC.StringValue as ComponentName,
		TC.FlightCommentID,
		TC.CommentRating,
		TC.PilotComment
	FROM
		Program AS P
		INNER JOIN Aircraft AS V ON
			V.ProgId = P.ProgId
		INNER JOIN Flight AS T ON
			T.AircraftId = V.AircraftId
		INNER JOIN Pilot AS D ON
			D.Id = T.PilotId
		INNER JOIN FlightComment AS TC ON 
			TC.FlightId = T.FlightId
		INNER JOIN AircraftComponent AS VC ON
			VC.Id = TC.ComponentId
		INNER JOIN Location AS L ON
			L.LocactionId = T.LocationId
	WHERE
		P.ProgId = @ProgId
		AND ((@AircraftId = -1) OR (V.AircraftId = @AircraftId))
		AND ((@LocationId = -1) OR (T.LocationId = @LocationId))
		AND Convert(DateTime,T.FlightDate) >= @DateFrom
		AND Convert(DateTime,T.FlightDate) <= @DateThru
		AND P.IsDeleted = 0 AND P.IsActive = 1
		AND V.IsDeleted = 0 AND V.IsActive = 1
		AND T.IsDeleted = 0 AND T.IsActive = 1
		AND TC.IsDeleted = 0 AND TC.IsActive = 1
	ORDER BY
	    T.AirframeHoursStart,
		T.FlightDateSort,
		T.FlightId,
		TC.FlightcommentId
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptFuelBurn]
	@ProgId INT,
	@AircraftId INT = -1,
	@DateFrom DATETIME,
	@DateThru DATETIME,
	@LiquidGroupId INTEGER 
AS
BEGIN
	SET NOCOUNT ON

	SELECT
		MIN(P.ProgName) AS ProgName,
		V.AircraftId,
		MIN(V.MSN) AS MSN,
		MIN(V.RegistrationNbr) AS RegistrationNbr,
		T.FlightId,
		MIN(Convert(DateTime, T.FlightDateSort)) AS FlightDate,
		MIN(L.LocationName) AS LocationName,
		MIN(T.AirframeHoursStart) AS MeterStart,
		MAX(T.AirframeHoursEnd) AS MeterEnd,
		DailyHours = MIN(T.AirframeHoursEnd) - MAX(T.AirframeHoursStart),
		SUM(LF.LiquidAmt) AS DailyFuel
	FROM
		Aircraft AS V
		INNER JOIN Program AS P ON 
			P.ProgId = V.Progid
		INNER JOIN Flight AS T ON
			T.AircraftId = V.AircraftId 
			AND Convert(DateTime, T.FlightDateSort) >= @DateFrom 
			AND Convert(DateTime, T.FlightDatesort) <= @DateThru
		LEFT OUTER JOIN LiquidFill AS LF ON 
			LF.FlightId = T.FlightId
			AND LF.LiquidGroupId = @LiquidGroupId
			AND (ISNULL(LF.IsDeleted, 0) = 0 AND ISNULL(LF.IsActive, 1) = 1)
		LEFT OUTER JOIN Location AS L ON
			L.LocactionId = T.LocationId
	WHERE
		P.ProgId = @ProgId AND
		((@AircraftId = -1) OR (V.AircraftId = @AircraftId)) AND
		(P.IsDeleted = 0) AND
		(V.IsDeleted = 0 AND V.IsActive = 1) AND
		(T.IsDeleted = 0 AND T.IsActive = 1) AND
		(ISNULL(L.IsDeleted, 0) = 0)
	GROUP BY
		V.AircraftId,
		T.AirframeHoursStart,
		T.FlightDateSort,
		T.FlightId
	ORDER BY
		V.AircraftId,
		T.AirframeHoursStart,
		T.FlightDateSort,
		T.FlightId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rptFuelBurnByRefill]
	@ProgId INT,
	@AircraftId INT = -1,
	@DateFrom DATETIME,
	@DateThru DATETIME,
	@LiquidGroupId INTEGER 
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #flights (
		ID INT PRIMARY KEY identity(1,1),
		ProgName NVARCHAR(255),
		AircraftId INT,
		MSN VARCHAR(20),
		RegistrationNbr VARCHAR(20),
		FlightId INT,
		FlightDate DATETIME,
		LocationName NVARCHAR(255),
		WorkShiftName NVARCHAR(255),
		SchedRouteName NVARCHAR(255),
		FlownRouteName NVARCHAR(255),
		FlightMeterStart DECIMAL(9,1),
		FlightMeterEnd DECIMAL(9,1),
		MeterUnit NVARCHAR(255),
		LiquidFillId INT,
		HobbsReading DECIMAL(9,1),
		AirframeHoursReading DECIMAL(9,1),
		LiquidGroup NVARCHAR(20),
		LiquidAmt DECIMAL(6,3),
		LiquidUnit NVARCHAR(255)
		)
		
	CREATE TABLE #fills (
		ID INT PRIMARY KEY IDENTITY(1,1),
		ProgName NVARCHAR(255),
		AircraftId INT,
		MSN VARCHAR(20),
		RegistrationNbr VARCHAR(20),
		FlightDate DATETIME,
		LocationName NVARCHAR(20),
		WorkShiftName NVARCHAR(10),
		SchedRouteName NVARCHAR(30),
		FlownRouteName NVARCHAR(30),
		FlightMeterSort DECIMAL(9,1),
		FlightMeterStart DECIMAL(9,1) NULL,
		FlightMeterEnd DECIMAL(9,1) NULL,
		FlightAirtime DECIMAL(9,1) NULL,
		TotalFlightAirtime DECIMAL(9,1) NULL,
		MeterUnit NVARCHAR(20),
		LiquidFillId INT NULL,
		RefillMeterLast DECIMAL(9,1) NULL,
		RefillMeterCurrent DECIMAL(9,1) NULL,
		RefillAirtime DECIMAL(9,1) NULL,
		LiquidGroup NVARCHAR(20) NULL,
		LiquidAmt DECIMAL(18,3) NULL,
		LiquidUnit NVARCHAR(20) NULL,
		Usage DECIMAL(12,3) NULL,
		TotalRefillAirtime DECIMAL(9,1) NULL,
		TotalLiquidAmt DECIMAL(18,3) NULL,
		AvgUsage DECIMAL(6,3) NULL
		)
		
	INSERT INTO #flights (
		ProgName,
		AircraftId,
		MSN,
		RegistrationNbr,
		FlightId,
		FlightDate,
		LocationName,
		WorkShiftName,
		SchedRouteName,
		FlownRouteName,
		FlightMeterStart,
		FlightMeterEnd,
		LiquidFillId,
		HobbsReading,
		AirframeHoursReading,
		MeterUnit,
		LiquidGroup,
		LiquidAmt,
		LiquidUnit
		)
	SELECT
		P.ProgName AS ProgName,
		V.AircraftId,
		V.MSN AS MSN,
		V.RegistrationNbr AS RegistrationNbr,
		T.FlightId,
		Convert(DateTime, T.FlightDateSort) AS FlightDate,
		L.LocationName AS LocationName,
		WS.StringValue AS WorkShiftName,
		SR.Name AS SchedRouteName,
		DR.Name AS FlownRouteName,
		T.AirframeHoursStart,
		T.AirframeHoursEnd,
		ISNULL(LF.LiquidFillId, 0) AS LiquidFillId,
		ISNULL(LF.HobbsReading, 0) AS HobbsReading,
		ISNULL(LF.AirframeHoursReading, 0) AS AirframeHoursReading,
		ISNULL(LeU.StringValue, '') AS MeterUnit,
		ISNULL(LG.StringValue, '') AS LiquidGroup,
		ISNULL(LF.LiquidAmt, 0) AS LiquidAmt,
		ISNULL(LiU.StringValue, '') AS LiquidUnit
	FROM
		Aircraft AS V
		INNER JOIN Program AS P ON 
			P.ProgId = V.Progid
		INNER JOIN Flight AS T ON
			T.AircraftId = V.AircraftId 
		INNER JOIN dbo.[Route] AS SR ON
			SR.RouteId = T.SchedRouteId
		INNER JOIN dbo.[Route] AS DR ON
			DR.RouteId = T.FlownRouteId
		INNER JOIN dbo.WorkShift AS WS ON
			WS.ID = T.WorkShiftIndicatorId
		INNER JOIN LengthUnit AS LeU ON
			LeU.ID = T.MeterUnitId
		LEFT OUTER JOIN LiquidFill AS LF ON 
			LF.FlightId = T.FlightId
			AND LF.LiquidGroupId = @LiquidGroupId
			AND (ISNULL(LF.IsDeleted, 0) = 0 AND ISNULL(LF.IsActive, 1) = 1) 
		LEFT OUTER JOIN LiquidGroup AS LG ON
			LG.ID = LF.LiquidGroupId
		LEFT OUTER JOIN LiquidUnit AS LiU ON
			LiU.ID = LF.LiquidUnitId
		INNER JOIN Location AS L ON
			L.LocactionId = T.LocationId
	WHERE
		P.ProgId = @ProgId AND
		((@AircraftId = -1) OR (V.AircraftId = @AircraftId)) AND
		(P.IsDeleted = 0) AND
		(V.IsDeleted = 0 AND V.IsActive = 1) AND
		(T.IsDeleted = 0 AND T.IsActive = 1) AND
		(ISNULL(L.IsDeleted, 0) = 0)
	ORDER BY
		V.AircraftId,
		T.HobbsStart,
		T.FlightDateSort,
		T.FlightId,
		LF.AirframeHoursReading

	DECLARE @ID INT
	DECLARE @ProgName NVARCHAR(255)
	DECLARE @cAircraftId INT
	DECLARE @MSN VARCHAR(20)
	DECLARE @RegistrationNbr VARCHAR(20)
	DECLARE @FlightId INT
	DECLARE @FlightDate DATETIME
	DECLARE @LocationName NVARCHAR(255)
	DECLARE @WorkShiftName NVARCHAR(10)
	DECLARE @SchedRouteName NVARCHAR(20)
	DECLARE @FlownRouteName NVARCHAR(20)
	DECLARE @FlightMeterStart DECIMAL(9,1)
	DECLARE @FlightMeterEnd DECIMAL(9,1)
	DECLARE @MeterUnit AS NVARCHAR(255)
	DECLARE @LiquidFillId INT
	DECLARE @HobbsReading DECIMAL(9,1)
	DECLARE @AirframeHoursReading DECIMAL(9,1)
	DECLARE @LiquidGroup AS NVARCHAR(20)
	DECLARE @LiquidAmt AS DECIMAL(6,3)
	DECLARE @LiquidUnit AS NVARCHAR(255)
	
	DECLARE @LastAircraftId INT
	DECLARE @LastFlightId INT
	DECLARE @ReFillMeterLast DECIMAL(18,1)
	DECLARE @ReFillMeterCurrent DECIMAL(18,1)
	DECLARE @TotalFlightAirtime DECIMAL(18,1)
	DECLARE @TotalRefillAirtime DECIMAL(18,1)
	DECLARE @TotalLiquidAmt DECIMAL(18,3)
	
	
	DECLARE curFlight CURSOR FOR
		SELECT *
		FROM #flights 
		ORDER BY 
			AircraftId, 
			FlightDate, 
			FlightMeterStart,
			AirframeHoursReading
	
	OPEN curFlight
	FETCH NEXT FROM curFlight INTO 
		@ID,
		@ProgName,
		@cAircraftId,
		@MSN,
		@RegistrationNbr,
		@FlightId,
		@FlightDate,
		@LocationName,
		@WorkShiftName,
		@SchedRouteName,
		@FlownRouteName,
		@FlightMeterStart,
		@FlightMeterEnd,
		@MeterUnit,
		@LiquidFillId,
		@HobbsReading,
		@AirframeHoursReading,
		@LiquidGroup,
		@LiquidAmt,
		@LiquidUnit
	
	SELECT @LastAircraftId = @cAircraftId
	SELECT @LastFlightId = 0
	SELECT @TotalFlightAirtime = 0
	SELECT @TotalRefillAirtime = 0
	SELECT @TotalLiquidAmt = 0
	
	SELECT @RefillMeterLast = @FlightMeterStart	
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		IF @cAircraftId <> @LastAircraftId 
		BEGIN 
			SELECT @RefillMeterLast = @FlightMeterStart
			SELECT @TotalFlightAirtime = 0
			SELECT @TotalRefillAirtime = 0
			SELECT @TotalLiquidAmt = 0
		END

		IF @LastFlightid <> @FlightID 
		BEGIN 
			SELECT @TotalFlightAirtime = @TotalFlightAirtime + (@FlightMeterEnd - @FlightMeterStart)
		END
		
		IF @LiquidFillId <> 0 
		BEGIN
			
			IF IsNull(@AirframeHoursReading, 0) = 0
				SELECT @RefillMeterCurrent = IsNull(@HobbsReading, 0)
			ELSE
				SELECT @RefillMeterCurrent = IsNull(@AirframeHoursReading, 0)
			
			SELECT @TotalRefillAirtime = @TotalRefillAirtime + (@RefillMeterCurrent - @ReFillMeterLast)			
			SELECT @TotalLiquidAmt = @TotalLiquidAmt + IsNull(@LiquidAmt, 0)
			

			INSERT INTO #fills (
				ProgName,
				AircraftId,
				MSN,
				RegistrationNbr,
				FlightDate,
				LocationName,
				WorkShiftName,
				SchedRouteName,
				FlownRouteName,
				FlightMeterSort,
				FlightMeterStart,
				FlightMeterEnd,
				FlightAirtime,
				TotalFlightAirtime,
				MeterUnit,
				LiquidFillId,
				RefillMeterLast,
				RefillMeterCurrent,
				RefillAirtime,
				LiquidGroup,
				LiquidAmt,
				LiquidUnit,
				Usage,
				TotalRefillAirtime,
				TotalLiquidAmt,
				AvgUsage
				)
			VALUES (
				@ProgName,
				@cAircraftId,
				@MSN,
				@RegistrationNbr,
				@FlightDate,
				@LocationName,
				@WorkShiftName,
				@SchedRouteName,
				@FlownRouteName,
				@FlightMeterEnd,
				CASE
					WHEN @LastFlightid = @FlightId THEN NULL
					ELSE @FlightMeterStart
				END,
				CASE
					WHEN @LastFlightid = @FlightId THEN NULL
					ELSE @FlightMeterEnd
				END,
				CASE
					WHEN @LastFlightId = @FlightId THEN NULL
					ELSE @FlightMeterEnd - @FlightMeterStart
				END,
				CASE
					WHEN @LastFlightId = @FlightId THEN NULL
					ELSE @TotalFlightAirtime
				END, 
				@MeterUnit,
				@LiquidFillId,
				@RefillMeterLast,
				@ReFillMeterCurrent,
				@ReFillMeterCurrent - @RefillMeterLast,
				@LiquidGroup,
				@LiquidAmt,
				@LiquidUnit, 
				CASE 
					WHEN @LiquidAmt = 0 THEN 0
					ELSE (@ReFillMeterCurrent - @RefillMeterLast) / @LiquidAmt	
				END,
				@TotalRefillAirtime,
				@TotalLiquidAmt,
				CASE
					WHEN IsNull(@TotalLiquidAmt, 0) = 0 then 0
					ELSE @TotalRefillAirtime / @TotalLiquidAmt
				END
				)
			
			SELECT @ReFillMeterLast = @ReFillMeterCurrent
		END
		ELSE
		BEGIN
			INSERT INTO #fills (
				ProgName,
				AircraftId,
				MSN,
				RegistrationNbr,
				FlightDate,
				LocationName,
				WorkShiftName,
				SchedRouteName,
				FlownRouteName,
				FlightMeterSort,
				FlightMeterStart,
				FlightMeterEnd,
				FlightAirtime,
				TotalFlightAirtime,
				MeterUnit		
				)
			VALUES (
				@ProgName,
				@cAircraftId,
				@MSN,
				@RegistrationNbr,
				@FlightDate,
				@LocationName,
				@WorkShiftName,
				@SchedRouteName,
				@FlownRouteName,
				@FlightMeterEnd,
				@FlightMeterStart,
				@FlightMeterEnd,
				@FlightMeterEnd - @FlightMeterStart,
				@TotalFlightAirtime,
				@MeterUnit
				)			
		END
		SELECT @LastAircraftId = @cAircraftId
		SELECT @LastFlightId = @FlightId
		
		FETCH NEXT FROM curFlight INTO 
			@ID,
			@ProgName,
			@cAircraftId,
			@MSN,
			@RegistrationNbr,
			@FlightId,
			@FlightDate,
			@LocationName,
			@WorkShiftName,
			@SchedRouteName,
			@FlownRouteName,
			@FlightMeterStart,
			@FlightMeterEnd,
			@MeterUnit,
			@LiquidFillId,
			@HobbsReading,
			@AirframeHoursReading,
			@LiquidGroup,
			@LiquidAmt,
			@LiquidUnit
		
	END
	
	CLOSE curFlight
	DEALLOCATE curFlight
	
	SELECT * FROM #fills
	WHERE ((FlightDate >= @DateFrom ) AND (FlightDate <= @DateThru))

	DROP TABLE #flights
	DROP TABLE #fills
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptFlightData]
	@ProgId INT,
	@LocationId INT = 0,
	@AircraftId INT = 0,
	@DateRangeStart DATETIME = NULL,
	@DateRangeEnd DATETIME = NULL,
	@FuelLiquidGroupId INT = 0,
	@OilLiquidGroupId INT = 0,
	@FluidsLiquidGroupId INT = 0
AS
BEGIN
	SET NOCOUNT ON
SELECT
	P.ProgName,
	V.AircraftId,
	V.MSN,
	V.RegistrationNbr,
	T.FlightId,
	T.FlightDate,
    WS.StringValue as Shift,
    T.DepartureTime,
    T.ReturnTime,
	L.LocationName,
    SR.Name as SchedRouteName,
	DR.Name as FlownRouteName,
    DV.StringValue as PilotName,
	T.AirframeHoursStart AS MeterStart,
	T.AirframeHoursEnd AS MeterEnd,
	DailyHours = (T.AirframeHoursEnd - T.AirframeHoursStart),
    isnull(LF1.LiquidAmt,0) as Fuel,
    isnull(LF2.LiquidAmt,0) as Oil,
    isnull(LF3.LiquidAmt,0) as Fluids,
    isnull(VC.StringValue,'') as Component,
    isnull(TC.PilotComment,'') as PilotComment,
	isnull(TC.CommentRating,0) as CommentRating
FROM
	Aircraft AS V
	INNER JOIN Program AS P ON 
		P.ProgId = V.Progid
	INNER JOIN Flight AS T ON
		T.AircraftId = V.AircraftId 
	LEFT OUTER JOIN WorkShift AS WS ON
		WS.Id = T.WorkShiftIndicatorId
	LEFT OUTER JOIN Location AS L ON
		L.LocactionId = T.LocationId
	LEFT OUTER JOIN Route AS SR ON
		SR.RouteId = T.SchedRouteId
	LEFT OUTER JOIN Route AS DR ON
		DR.RouteId = T.FlownRouteId
	LEFT OUTER JOIN Pilot AS DV ON
		DV.Id = T.PilotId
	LEFT OUTER JOIN (SELECT FlightId, LiquidGroupId, SUM(LiquidAmt) AS LiquidAmt FROM LiquidFill WHERE isActive = 1 AND Isdeleted = 0 GROUP BY FlightId, LiquidGroupId) AS LF1 ON 
		LF1.FlightId = T.FlightId
		AND LF1.LiquidGroupId = @FuelLiquidGroupId
	LEFT OUTER JOIN (SELECT FlightId, LiquidGroupId, SUM(LiquidAmt) AS LiquidAmt FROM LiquidFill WHERE isactive = 1 AND isdeleted = 0 GROUP BY FlightId, LiquidGroupId) AS LF2 ON 
		LF2.FlightId = T.FlightId
		AND LF2.LiquidGroupId = @OilLiquidGroupId
	LEFT OUTER JOIN (SELECT FlightId, LiquidGroupId, SUM(LiquidAmt) AS LiquidAmt FROM LiquidFill WHERE isactive = 1 AND isdeleted = 0 GROUP BY FlightId, LiquidGroupId) AS LF3 ON 
		LF3.FlightId = T.FlightId
		AND LF3.LiquidGroupId = @FluidsLiquidGroupId
	LEFT OUTER JOIN FlightComment AS TC ON 
		TC.FlightId = T.FlightId
	LEFT OUTER JOIN AircraftComponent AS VC ON 
		VC.Id = TC.ComponentId
WHERE
	P.ProgId = @ProgId 
	AND ((@LocationId = 0) OR (L.LocactionId = @LocationId))
	AND ((@DateRangeStart IS NULL) OR (@DateRangeStart <= CONVERT(DATETIME, T.FlightDate)))
	AND ((@DateRangeEnd IS NULL) OR (@DateRangeEnd >= CONVERT(DATETIME, T.FlightDate)))
	AND ((@AircraftId = 0) OR (V.AircraftId = @AircraftId))
	AND (P.IsDeleted = 0 AND P.IsActive = 1)
	AND (V.IsDeleted = 0 AND P.IsActive = 1)
	AND (T.IsDeleted = 0 AND T.IsActive = 1)
ORDER BY
	T.AirframeHoursStart,
	T.FlightDateSort,
	T.DepartureTime
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptFlightOutliers] 
	@ProgId INT,
	@DateBegin VARCHAR(10),
	@DateEnd VARCHAR(10),
	@NbrOfStDevs INT
AS

SET NOCOUNT ON

CREATE TABLE #tempStats (
	tempId INT IDENTITY,
	LocationId INT, 
	RouteId INT,
	MeterUnitId INT,
	FlightCount INT,
	FlightMinutesMin INT,
	FlightMinutesMax INT,
	FlightMinutesAvg INT,
	FlightMinutesStDev INT,
	FlightAirtimeMin DECIMAL(9,1),
	FlightAirtimeMax DECIMAL(9,1),
	FlightAirtimeAvg DECIMAL(9,1),
	FlightAirtimeStDev DECIMAL(9,1)
	)
INSERT INTO #tempStats (
	LocationId,
	RouteId,
	MeterUnitId,
	FlightCount, 
	FlightMinutesMin,
	FlightMinutesMax,
	FlightMinutesAvg,
	FlightMinutesStDev,
	FlightAirtimeMin,
	FlightAirtimeMax,
	FlightAirtimeAvg,
	FlightAirtimeStDev
	)	
SELECT 
	Location.LocactionId,
	A.FlownRouteId,
	A.MeterUnitId,
	COUNT(A.FlightId) AS FlightCount,
	MIN(a.FlightMinutes) AS FlightMinutesMin,
	MAX(A.FlightMinutes) AS FlightMinutesMax,
	AVG(A.FlightMinutes) AS FlightMinutesAvg,
	ISNULL(STDEV(A.FlightMinutes), 0) AS FlightMinutesStDev,
	MIN(A.FlightAirtime) AS FlightAirtimeMin,
	MAX(A.FlightAirtime) AS FlightAirtimeMax,
	AVG(A.FlightAirtime) AS FlightAirtimeAvg,
	ISNULL(STDEV(A.FlightAirtime), 0) AS FlightAirtimeStDev
	
FROM
	(SELECT 
		Flight.FlightId,
		Flight.FlownRouteID,
		Flight.MeterUnitId,
		Flight.ReturnDateTime_Local,
		Flight.DepartureDateTime_Local,
		DATEDIFF(minute, Flight.DepartureDateTime_Local, Flight.ReturnDateTime_Local) AS FlightMinutes,
		(Flight.HobbsEnd - Flight.HobbsStart) AS FlightAirtime
	FROM 
		dbo.Flight
		INNER JOIN dbo.Aircraft ON Aircraft.AircraftId = Flight.AircraftId
		INNER JOIN dbo.Program ON Program.ProgId = Aircraft.ProgId
	WHERE Program.ProgId = @ProgID AND Flight.FlightDateSort >= @DateBegin AND Flight.FlightDateSort <= @DateEnd) AS A
	INNER JOIN dbo.[ROUTE] ON [ROUTE].RouteId = A.FlownRouteId
	INNER JOIN dbo.Location ON Location.LocactionId = [Route].LocationId
GROUP BY 
	Location.LocationName,
	Location.LocactionId,
	[Route].NAME,
	A.FlownRouteId,
	A.MeterUnitId
ORDER BY 
	Location.LocationName,
	[Route].NAME
	
SELECT 
	Program.ProgName,
	Flight.LocationId,
	Location.LocationName,
	Flight.SchedRouteId,
	SchedRoute.NAME AS SchedRouteName,
	Flight.FlownRouteId,
	FlownRoute.NAME AS FlownRouteName,
	Flight.AircraftId,
	Aircraft.MSN,
	Flight.FlightId,
	Flight.PilotId,
	[Pilot].StringValue AS PilotName,
	CASE 
		WHEN ABS(DATEDIFF(minute, Flight.ReturnDateTime_Local, Flight.DepartureDateTime_Local)) > (#tempStats.FlightMinutesAvg + (@NbrOfStDevs * #tempStats.FlightMinutesStDev)) THEN 1
		ELSE 0
		END AS FlightTimeOutlier,
	CASE 
		WHEN ABS(Flight.HobbsEnd - Flight.HobbsStart) > (#tempStats.FlightAirtimeAvg + (CONVERT(DECIMAL(9,1), @NbrOfStDevs) * #tempStats.FlightAirtimeStDev)) THEN 1
		ELSE 0
		END AS FlightAirtimeOutlier,
	#tempStats.FlightCount,
	Flight.FlightDateSort,
	Flight.DepartureTime,
	Flight.ReturnTime,
	DATEDIFF(minute, Flight.DepartureDateTime_Local, Flight.ReturnDateTime_Local) AS FlightMinutes,
	#tempStats.FlightMinutesAvg,
	#tempStats.FlightMinutesStDev,
	Flight.HobbsStart,
	Flight.HobbsEnd,
	Flight.MeterUnitId,
	LengthUnit.StringValue AS MeterUnit,
	Flight.HobbsEnd - Flight.HobbsStart AS FlightAirtime,
	#tempStats.FlightAirtimeAvg,
	#TempStats.FlightAirtimeStDev
FROM
	Flight
	INNER JOIN Location ON Location.LocactionId = Flight.LocationId
	INNER JOIN Aircraft ON Aircraft.AircraftId = Flight.AircraftId
	INNER JOIN Program ON Program.ProgId = Location.ProgId
	INNER JOIN [Route] AS SchedRoute ON SchedRoute.RouteId = Flight.SchedRouteId
	INNER JOIN [Route] AS FlownRoute ON FlownRoute.RouteId = Flight.FlownRouteId
	INNER JOIN [Pilot] ON [Pilot].Id = Flight.PilotId
	INNER JOIN [LengthUnit] ON LengthUnit.Id = Flight.MeterUnitId
	INNER JOIN #tempStats ON #tempStats.LocationId = Flight.LocationId AND #tempStats.RouteId = Flight.FlownRouteId AND #tempStats.MeterUnitId = Flight.MeterUnitId
WHERE
	Flight.FlightDateSort >= @DateBegin AND Flight.FlightDateSort <= @DateEnd
	AND Location.ProgId = @ProgId
	AND (ABS(DATEDIFF(minute, Flight.ReturnDateTime_Local, Flight.DepartureDateTime_Local)) > (#tempStats.FlightMinutesAvg + (@NbrOfStDevs * #tempStats.FlightMinutesStDev))
		 OR ABS(Flight.HobbsEnd - Flight.HobbsStart) > (#tempStats.FlightAirtimeAvg + (CONVERT(DECIMAL(9,1), @NbrOfStDevs) * #tempStats.FlightAirtimeStDev)))
ORDER BY
	Location.LocationName,
	SchedRouteName,
	FlownRouteName,
	Flight.DepartureDateTime_Local
RETURN 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptFlightStatistics] 
	@ProgId INT,
	@DateBegin VARCHAR(10),
	@DateEnd VARCHAR(10)
AS

SELECT 
	A.ProgName,
	Location.LocactionId,
	Location.LocationName,
	A.FlownRouteId,
	[ROUTE].Name AS RouteName,
	A.MeterUnitId,
	MIN(LengthUnit.StringValue) AS MeterUnit,
	COUNT(A.FlightId) AS FlightCount,
	MIN(a.FlightMinutes) AS FlightMinutesMin,
	MAX(A.FlightMinutes) AS FlightMinutesMax,
	AVG(A.FlightMinutes) AS FlightMinutesAvg,
	ISNULL(STDEV(A.FlightMinutes), 0) AS FlightMinutesStDev,
	MIN(A.FlightAirtime) AS FlightAirtimeMin,
	MAX(A.FlightAirtime) AS FlightAirtimeMax,
	AVG(A.FlightAirtime) AS FlightAirtimeAvg,
	ISNULL(STDEV(A.FlightAirtime), 0) AS FlightAirtimeStDev
	
FROM
	(SELECT 
		Program.ProgName,
		Flight.FlightId,
		Flight.FlownRouteID,
		Flight.MeterUnitId,
		Flight.ReturnDateTime_Local,
		Flight.DepartureDateTime_Local,
		DATEDIFF(minute, Flight.DepartureDateTime_Local, Flight.ReturnDateTime_Local) AS FlightMinutes,
		(Flight.HobbsEnd - Flight.HobbsStart) AS FlightAirtime
	FROM 
		dbo.Flight
		INNER JOIN dbo.Aircraft ON Aircraft.AircraftId = Flight.AircraftId
		INNER JOIN dbo.Program ON Program.ProgId = Aircraft.ProgId
	WHERE Program.ProgId = @ProgID AND Flight.FlightDateSort >= @DateBegin AND Flight.FlightDateSort <= @DateEnd) AS A
	INNER JOIN dbo.[ROUTE] ON [ROUTE].RouteId = A.FlownRouteId
	INNER JOIN dbo.Location ON Location.LocactionId = [Route].LocationId
	INNER JOIN LengthUnit ON dbo.LengthUnit.Id = A.MeterUnitId
GROUP BY 
	A.ProgName,
	Location.LocationName,
	Location.LocactionId,
	[Route].NAME,
	A.FlownRouteId,
	MeterUnitId
ORDER BY 
	Location.LocationName,
	[Route].NAME

RETURN 
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[rptAircraftComments]
	@ProgId INT,
	@LocationId INT = -1,
	@AircraftId INT = -1,
	@ComponentId INT = -1,
	@RatingLow SMALLINT = 0,
	@RatingHigh SMALLINT = 10,
	@DateFrom DATETIME,
	@DateThru DATETIME
AS
BEGIN
	SET NOCOUNT ON

	CREATE TABLE #temp (
		tempID INT IDENTITY PRIMARY KEY,
		ProgName NVARCHAR(255),
		LocationName NVARCHAR(255),
		LocationId INT,
		MSN NVARCHAR(20),
		AircraftId INT,
		CommentDate DATETIME,
		SourceId INT,
		Source NVARCHAR(255),
		CommentAuthor NVARCHAR(255),
		CommentRating SMALLINT,
		Component NVARCHAR(255),
		ComponentId INT,
		Comment NVARCHAR(2048)
		)		
		
	INSERT INTO #temp (
		ProgName,
		LocationName,
		LocationId,
		MSN,
		AircraftId,
		CommentDate,
		SourceId,
		Source,
		CommentAuthor,
		CommentRating,
		Component,
		ComponentId,
		Comment
	)
	SELECT
		Program.ProgName,
		Location.LocationName,
		Location.LocactionId,
		Aircraft.MSN,
		Aircraft.AircraftId,
		AircraftComment.CommentDate,
		AircraftCommentId,
		'Aircraft',
		Engineer.StringValue AS EngineerName,
		AircraftComment.CommentRating,
		AircraftComponent.StringValue AS Component,
		AircraftComponent.Id AS ComponentId,
		AircraftComment.Comments
	FROM
		Aircraft
		INNER JOIN Program ON Aircraft.ProgId = Program.ProgID
		INNER JOIN Location ON Location.LocactionId = Aircraft.CurrentLocationId
		INNER JOIN AircraftComment ON AircraftComment.AircraftId = Aircraft.AircraftId
		INNER JOIN Engineer ON Engineer.ID = AircraftComment.EngineerId
		INNER JOIN AircraftComponent ON AircraftComponent.id = AircraftComment.ComponentId
	WHERE
		Program.ProgId = @ProgID
		AND ((@AircraftId = -1) OR (Aircraft.AircraftId = @AircraftId))
		AND ((@LocationId = -1) OR (Location.LocactionId = @LocationId))
		AND ((@ComponentId = -1) OR (AircraftComponent.Id = @ComponentId))
		AND (AircraftComment.CommentRating >= @RatingLow)
		AND (AircraftComment.CommentRating <= @RatingHigh)
		AND (AircraftComment.CommentDate >= @DateFrom)
		AND (AircraftComment.CommentDate <= @DateThru)
		AND Aircraft.IsDeleted = 0
		AND AircraftComment.IsDeleted = 0

	INSERT INTO #temp (
		ProgName,
		LocationName,
		LocationId,
		MSN,
		AircraftId,
		CommentDate,
		SourceId,
		Source,
		CommentAuthor,
		CommentRating,
		Component,
		ComponentId,
		Comment
	)
	SELECT
		Program.ProgName,
		Location.LocationName,
		Location.LocactionId,
		Aircraft.Msn,
		Aircraft.AircraftId,
		WOEngineerComment.CommentDate,
		WOEngineerComment.EngineerCommentsId,
		'Engineer WO',
		Engineer.StringValue AS EngineerName,
		WOEngineerComment.CommentRating,
		AircraftComponent.StringValue AS Component,
		AircraftComponent.Id AS ComponentId,
		WOEngineerComment.Comments
	FROM
		WorkOrder
		INNER JOIN Program ON Program.ProgId = WorkOrder.ProgId
		INNER JOIN Aircraft ON Aircraft.AircraftId = WorkOrder.AircraftId
		INNER JOIN Location ON Location.LocactionId = WorkOrder.LocationId
		INNER JOIN WOEngineerComment ON WOEngineerComment.WorkOrderId = WorkOrder.WorkOrderId
		INNER JOIN Engineer ON Engineer.id = WorkOrder.EngineerId
		INNER JOIN AircraftComponent ON AircraftComponent.Id = WOEngineerComment.ComponentId
	WHERE
		Program.ProgId = @ProgId
		AND ((@AircraftId = -1) OR (Aircraft.AircraftId = @AircraftId))
		AND ((@LocationId = -1) OR (Location.LocactionId = @LocationId))
		AND ((@ComponentId = -1) OR (AircraftComponent.Id = @ComponentId))
		AND (WOEngineerComment.CommentRating >= @RatingLow)
		AND (WOEngineerComment.CommentRating <= @RatingHigh)
		AND (WOEngineerComment.CommentDate >= @DateFrom)
		AND (WOEngineerComment.CommentDate <= @DateThru)
		AND Aircraft.IsDeleted = 0
		AND WorkOrder.IsDeleted = 0
		AND dbo.WOEngineerComment.IsDeleted = 0
		
	INSERT INTO #temp (
		ProgName,
		LocationName,
		LocationId,
		MSN,
		AircraftId,
		CommentDate,
		SourceId,
		Source,
		CommentAuthor,
		CommentRating,
		Component,
		ComponentId,
		Comment
	)
	SELECT
		Program.ProgName,
		Location.LocationName,
		Location.LocactionId,
		Aircraft.Msn,
		Aircraft.AircraftId,
		WOMechanicComment.CommentDate,
		WOMechanicComment.WOMechanicCommentsId,
		'Svc Tech WO',
		Mechanic.StringValue AS EngineerName,
		WOMechanicComment.CommentRating,
		AircraftComponent.StringValue AS Component,
		AircraftComponent.Id AS ComponentId,
		WOMechanicComment.Comments
	FROM
		WorkOrder
		INNER JOIN Program ON Program.ProgId = WorkOrder.ProgId
		INNER JOIN Aircraft ON Aircraft.AircraftId = WorkOrder.AircraftId
		INNER JOIN Location ON Location.LocactionId = WorkOrder.LocationId
		INNER JOIN WOMechanicComment ON WOMechanicComment.WorkOrderId = WorkOrder.WorkOrderId
		INNER JOIN Mechanic ON Mechanic.id = WorkOrder.MechanicId
		INNER JOIN AircraftComponent ON AircraftComponent.Id = WOMechanicComment.ComponentId
	WHERE
		Program.ProgId = @ProgId
		AND ((@AircraftId = -1) OR (Aircraft.AircraftId = @AircraftId))
		AND ((@LocationId = -1) OR (Location.LocactionId = @LocationId))
		AND ((@ComponentId = -1) OR (AircraftComponent.Id = @ComponentId))
		AND (WOMechanicComment.CommentRating >= @RatingLow)
		AND (WOMechanicComment.CommentRating <= @RatingHigh)
		AND (WOMechanicComment.CommentDate >= @DateFrom)
		AND (WOMechanicComment.CommentDate <= @DateThru)
		AND Aircraft.IsDeleted = 0
		AND WorkOrder.IsDeleted = 0
		AND dbo.WOMechanicComment.IsDeleted = 0

	INSERT INTO #temp (
		ProgName,
		LocationName,
		LocationId,
		MSN,
		AircraftId,
		CommentDate,
		SourceId,
		Source,
		CommentAuthor,
		CommentRating,
		Component,
		ComponentId,
		Comment
	)
	SELECT
		P.ProgName,
		L.LocationName,
		L.LocactionId,
		V.MSN,
		V.AircraftId,
		T.FlightDate,
		TC.FlightCommentID,
		'Flight',
		D.StringValue AS PilotName,
		TC.CommentRating,
		VC.StringValue as ComponentName,
		VC.Id AS ComponentId,
		TC.PilotComment
	FROM
		Program AS P
		INNER JOIN Aircraft AS V ON
			V.ProgId = P.ProgId
		INNER JOIN Flight AS T ON
			T.AircraftId = V.AircraftId
		INNER JOIN Pilot AS D ON
			D.Id = T.PilotId
		INNER JOIN FlightComment AS TC ON 
			TC.FlightId = T.FlightId
		INNER JOIN AircraftComponent AS VC ON
			VC.Id = TC.ComponentId
		INNER JOIN Location AS L ON
			L.LocactionId = T.LocationId
	WHERE
		P.ProgId = @ProgId
		AND ((@AircraftId = -1) OR (V.AircraftId = @AircraftId))
		AND ((@LocationId = -1) OR (T.LocationId = @LocationId))
		AND ((@ComponentId = -1) OR (TC.ComponentId = @ComponentId))
		AND Convert(DateTime,T.FlightDate) >= @DateFrom
		AND Convert(DateTime,T.FlightDate) <= @DateThru
		AND TC.CommentRating >= @RatingLow
		AND TC.CommentRating <= @RatingHigh
		AND V.IsDeleted = 0
		AND T.IsDeleted = 0
		AND TC.IsDeleted = 0
	ORDER BY
		T.FlightDateSort,
		T.FlightId,
		TC.FlightcommentId
		
	SELECT
		ProgName,
		LocationName,
		LocationId,
		MSN,
		AircraftId,
		CommentDate,
		SourceId,
		Source,
		CommentAuthor,
		CommentRating,
		Component,
		ComponentId,
		Comment
	FROM #temp
	ORDER BY
		CommentDate,
		MSN,
		AircraftId,
		LocationName,
		LocationId,	
		Source,
		SourceId
		
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[rptAircraftData]
	@ProgId INT,
	@LocationId INT = 0,
	@AircraftId INT = 0
AS
BEGIN
	SET NOCOUNT ON
SELECT
	P.ProgName,
	V.AircraftId,
	V.MSN,
	V.RegistrationNbr,
    L.LocationName as CurrentLocation,
	V.ShipNbr,
	V.SerialNbr,
	M.StringValue as Model,
	V.Description,
	V.LiveryColor,
	E.StringValue as Engineer,
	ST.StringValue as Mechanic,
    VS.StringValue as AircraftStatus,
    Man.StringValue as Manufacturer,
	V.AirframeHoursStart,
	LU.StringValue as MeterUnit,
	V.TotalRequiredUsage,
	V.EstEndOfTesting,
	V.ActEndOfTesting,
	V.IsActive as ActiveStatus
FROM
	Aircraft AS V
	INNER JOIN Program AS P ON 
		P.ProgId = V.Progid
	LEFT OUTER JOIN Location AS L ON
		L.LocactionId = V.CurrentLocationId
	LEFT OUTER JOIN Model AS M ON
		M.Id = V.ModelId
	LEFT OUTER JOIN Engineer AS E ON
		E.Id = V.EngineerId
	LEFT OUTER JOIN Mechanic AS ST ON
		ST.Id = V.MechanicId
	LEFT OUTER JOIN AircraftStatusDef AS VS ON
		VS.Id = V.AircraftStatusId
	LEFT OUTER JOIN Manufacturer AS Man ON
		Man.Id = V.ManufacturerId
	LEFT OUTER JOIN LengthUnit AS LU ON
		LU.Id = V.MeterUnitId
WHERE
	P.ProgId = @ProgId 
	AND ((@LocationId = 0) OR (L.LocactionId = @LocationId))
	AND ((@AircraftId = 0) OR (V.AircraftId = @AircraftId))
	AND V.IsDeleted = 0 
	AND P.IsDeleted = 0
ORDER BY
	V.AircraftId
		
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[setMeters] 
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE dbo.Aircraft
	SET
		AirframeHoursCurrent = 
		IsNull((SELECT MAX(Flight.hobbsEnd)
		 FROM Flight 
		 WHERE Flight.AircraftId = Aircraft.AircraftId
		 GROUP BY Flight.AircraftId), 0)
	
	UPDATE dbo.Aircraft
	SET AirframeHoursOffset = 0
	
	UPDATE dbo.Flight
	SET
		AirframeHoursStart = HobbsStart,
		AirframeHoursEnd = HobbsEnd,
		UsedMeterOffset = 0
		
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAppSetting]
	@AppId int,
	@TimeZoneCode varchar(10),
	@SMTPServerAndPort varchar(255) = '',
	@SMTPAuthentication varchar(255) = '',
	@SMTPEnableSSL bit = 0,
	@SMTPUserName varchar(255) = '',
	@SMTPPassword varchar(255) = '',
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AppSetting]
	SET
		TimeZoneCode = @TimeZoneCode, 
		SMTPServerAndPort = @SMTPServerAndPort, 
		SMTPAuthentication = @SMTPAuthentication, 
		SMTPEnableSSL = @SMTPEnableSSL, 
		SMTPUserName = @SMTPUserName, 
		SMTPPassword = @SMTPPassword, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		AppId = @AppId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[AppSetting]
	WHERE 
		AppId = @AppId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAPUType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[APUType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[APUType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFltModule]
	@ModuleName nvarchar(50),
	@ModuleDescription nvarchar(255) = '',
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltModule]
	SET
		ModuleDescription = @ModuleDescription, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		ModuleName = @ModuleName AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FltModule]
	WHERE 
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFltRole]
	@ProgId Int,
	@RoleId int,
	@RoleName nvarchar(50),
	@RoleDescription nvarchar(255) = '',
	@IsProgramManager bit = 0,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltRole]
	SET
		ProgId = @ProgId, 
		RoleName = @RoleName, 
		RoleDescription = @RoleDescription, 
		IsProgramManager = @IsProgramManager, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		RoleId = @RoleId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FltRole]
	WHERE 
		RoleId = @RoleId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFltRoleModulePermissions]
	@RoleId Int,
	@ModuleName nvarchar(50),
	@CanView bit = 0,
	@CanEdit bit = 0,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltRoleModulePermissions]
	SET
		CanView = @CanView, 
		CanEdit = @CanEdit, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		RoleId = @RoleId AND 
		ModuleName = @ModuleName AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FltRoleModulePermissions]
	WHERE 
		RoleId = @RoleId AND 
		ModuleName = @ModuleName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFltUser]
	@UserName nvarchar(20),
	@Password nvarchar(128),
	@PasswordSalt nvarchar(128) = '',
	@Email nvarchar(255),
	@PasswordQuestion nvarchar(255) = '',
	@PasswordAnswer nvarchar(128) = '',
	@IsSuperUser bit = 0,
	@IsApproved bit = 1,
	@IsLockedOut bit = 0,
	@utcLastLoginDate datetime = Null,
	@utcLastLockoutDate datetime = Null,
	@utcLastPasswordChangedDate datetime = Null,
	@FailedPasswordAttemptCount Int = 0,
	@utcFailedPasswordAttemptWindowStart datetime = Null,
	@FailedPasswordAnswerAttemptCount Int = 0,
	@utcFailedPasswordAnswerAttemptWindowStart datetime = Null,
	@DisplayName nvarchar(30) = '',
	@Location nvarchar(30) = '',
	@CountryCode nvarchar(20) = '',
	@PhoneNumber nvarchar(20) = '',
	@MgrUserName nvarchar(20) = '',
	@Department nvarchar(30) = '',
	@Title nvarchar(30) = '',
	@Company nvarchar(30) = '',
	@DefaultProgId Int = 0,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FltUser]
	SET
		Password = @Password, 
		PasswordSalt = @PasswordSalt, 
		Email = @Email, 
		PasswordQuestion = @PasswordQuestion, 
		PasswordAnswer = @PasswordAnswer, 
		IsSuperUser = @IsSuperUser, 
		IsApproved = @IsApproved, 
		IsLockedOut = @IsLockedOut, 
		utcLastLoginDate = @utcLastLoginDate, 
		utcLastLockoutDate = @utcLastLockoutDate, 
		utcLastPasswordChangedDate = @utcLastPasswordChangedDate, 
		FailedPasswordAttemptCount = @FailedPasswordAttemptCount, 
		utcFailedPasswordAttemptWindowStart = @utcFailedPasswordAttemptWindowStart, 
		FailedPasswordAnswerAttemptCount = @FailedPasswordAnswerAttemptCount, 
		utcFailedPasswordAnswerAttemptWindowStart = @utcFailedPasswordAnswerAttemptWindowStart, 
		DisplayName = @DisplayName, 
		Location = @Location, 
		CountryCode = @CountryCode, 
		PhoneNumber = @PhoneNumber, 
		MgrUserName = @MgrUserName, 
		Department = @Department, 
		Title = @Title, 
		Company = @Company, 
		DefaultProgId = @DefaultProgId, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		UserName = @UserName AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FltUser]
	WHERE 
		UserName = @UserName

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateDocType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[DocType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[DocType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateDocument]
	@DocId int,
	@ProgId Int,
	@DocTypeId Int,
	@DocFileName varchar(255),
	@DocDescription nvarchar(255) = '',
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Document]
	SET
		ProgId = @ProgId, 
		DocTypeId = @DocTypeId, 
		DocFileName = @DocFileName, 
		DocDescription = @DocDescription, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		DocId = @DocId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Document]
	WHERE 
		DocId = @DocId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateDocumentInfo]
	@DocumentId int,
	@ProgId Int,
	@DocumentTypeId Int,
	@DocumentName varchar(255),
	@DocumentDescription nvarchar(255) = '',
	@DocumentSize INT,
	@IsDeleted bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[DocumentInfo]
	SET
		ProgId = @ProgId, 
		DocumentTypeId = @DocumentTypeId, 
		DocumentName = @DocumentName, 
		DocumentDescription = @DocumentDescription, 
		DocumentSize = @DocumentSize,
		IsDeleted = @IsDeleted, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		DocumentId = @DocumentId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[DocumentInfo]
	WHERE 
		DocumentId = @DocumentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateDocumentType]
	@DocumentTypeId int,
	@FileExtension VARCHAR(10),
	@MimeType VARCHAR(255),
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[DocumentType]
	SET
		FileExtension = @FileExtension,
		MimeType = @MimeType
	WHERE
		DocumentTypeId = @DocumentTypeId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[DocumentType]
	WHERE 
		DocumentTypeId = @DocumentTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateDotWriteDocument]
	@DocumentId AS INT,
	@DocumentContent AS VARBINARY(MAX)
AS
BEGIN
	SET NOCOUNT ON

	UPDATE dbo.document
	SET documentcontent .WRITE(@DocumentContent, NULL, NULL)
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updatePilot]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Pilot]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Pilot]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateEmission]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Emission]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Emission]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateEngineer]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Engineer]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Engineer]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateEngineeringAction]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[EngineeringAction]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[EngineeringAction]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateEngineType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[EngineType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[EngineType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateIssueCode]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[IssueCode]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[IssueCode]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLengthUnit]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LengthUnit]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LengthUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLiquidFill]
	@LiquidFillId int,
	@FlightId Int,
	@LiquidGroupId Int,
	@LiquidTypeId Int,
	@LiquidUnitId Int,
	@LiquidAmt DECIMAL(9,3),
	@HobbsReading DECIMAL(9,1),
	@AirframeHoursReading DECIMAL(9,1),
	@LiquidFillLocation nvarchar(255),
	@LiquidFillApprover NVARCHAR(30),
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LiquidFill]
	SET
		FlightId = @FlightId, 
		LiquidGroupId = @LiquidGroupId, 
		LiquidTypeId = @LiquidTypeId, 
		LiquidUnitId = @LiquidUnitId, 
		LiquidAmt = @LiquidAmt, 
		HobbsReading = @HobbsReading, 
		AirframeHoursReading = @AirframeHoursReading,
		LiquidFillLocation = @LiquidFillLocation, 
		LiquidFillApprover = @LiquidFillApprover,
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		LiquidFillId = @LiquidFillId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[LiquidFill]
	WHERE 
		LiquidFillId = @LiquidFillId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLiquidGroup]
	@ID int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LiquidGroup]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		ID = @ID AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidGroup]
	WHERE 
		ID = @ID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLiquidType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LiquidType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLiquidUnit]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LiquidUnit]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LiquidUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLocation]
	@LocactionId int,
	@ProgId Int,
	@TimeZoneId Int,
	@LocationName nvarchar(50),
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Location]
	SET
		ProgId = @ProgId, 
		TimeZoneId = @TimeZoneId, 
		LocationName = @LocationName, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		LocactionId = @LocactionId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Location]
	WHERE 
		LocactionId = @LocactionId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateManufacturer]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Manufacturer]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Manufacturer]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateMaturityDef]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[MaturityDef]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[MaturityDef]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateMechAction]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[MechAction]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[MechAction]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateModel]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Model]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Model]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateProgram]
	@ProgID int,
	@ProgName nvarchar(50),
	@ProgDescription nvarchar(255) = '',
	@ProgVersion nvarchar(50) = '',
	@ProgramLogo nvarchar(255) = '',
	@ReportLogo nvarchar(255) = '',
	@YearLogo nvarchar(255) = '',
	@ProgStatus tinyint = 0,
	@DateOpened varchar(50) = '',
	@DateClosed varchar(50) = '',
	@ActualValue money = 0,
	@ProjectCode varchar(50) = '',
	@ActivityCode varchar(50) = '',
	@DeptCode varchar(50) = '',
	@CompanyName nvarchar(255) = '',
	@NearEndUsage Int = 0,
	@AvgDailyUsage Int = 0,
	@WeeklyFlightDays Int = 0,
	@ProgramGuid uniqueIdentifier,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Program]
	SET
		ProgName = @ProgName, 
		ProgDescription = @ProgDescription, 
		ProgVersion = @ProgVersion, 
		ProgramLogo = @ProgramLogo, 
		ReportLogo = @ReportLogo, 
		YearLogo = @YearLogo, 
		ProgStatus = @ProgStatus, 
		DateOpened = @DateOpened, 
		DateClosed = @DateClosed, 
		ActualValue = @ActualValue, 
		ProjectCode = @ProjectCode, 
		ActivityCode = @ActivityCode, 
		DeptCode = @DeptCode, 
		CompanyName = @CompanyName, 
		NearEndUsage = @NearEndUsage, 
		AvgDailyUsage = @AvgDailyUsage, 
		WeeklyFlightDays = @WeeklyFlightDays, 
		ProgramGuid = @ProgramGuid, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		ProgID = @ProgID AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Program]
	WHERE 
		ProgID = @ProgID

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateRoute]
	@RouteId int,
	@LocationId Int,
	@Name nvarchar(50),
	@Description nvarchar(255),
	@TimeZoneId int,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Route]
	SET
		LocationId = @LocationId, 
		Name = @Name, 
		Description = @Description, 
		TimeZoneId = @TimeZoneId, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		RouteId = @RouteId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Route]
	WHERE 
		RouteId = @RouteId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateMechanic]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Mechanic]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Mechanic]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateRegion]
	@Id int,
	@ProgId Int,
	@Code nvarchar(2),
	@Name nvarchar(255),
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Region]
	SET
		ProgId = @ProgId, 
		Code = @Code, 
		Name = @Name, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Region]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateTest]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Test]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive 
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Test]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateLoadingConfig]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[LoadingConfig]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[LoadingConfig]
	WHERE 
		Id = @Id

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updatePropeller]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Propeller]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[Propeller]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updatePropellerType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[PropellerType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[PropellerType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFlight]
	@FlightId int,
	@AircraftId Int,
	@LocationId Int,
	@SchedRouteId Int,
	@FlownRouteId INT,
	@FlightDate varchar(10),
	@FlightDateSort VARCHAR(10),
	@WorkShiftIndicatorId Int,
	@AirframeWeightId INT = 0,
	@Cargo1WeightId INT = 0,
	@Cargo2WeightId INT = 0,
	@BallastWeightId INT = 0,
	@PilotId INT, 
	@PreFlightFtiOK smallint = 0,
	@PreFlightTelemetryOK smallint = 0,
	@PreFlightTelemetryCheckComplete smallint = 0,
	@OilLevelIndicatorStart smallint = 0,
	@HydraulicLevelIndicatorStart smallint = 0,
	@UsedMeterOffset DECIMAL(9,1) = 0,
	@MeterUnitId INT,
	@DepartureTime varchar(8),
	@DepartureDateTime_Local DATETIME,
	@DepartureDateTime_UTC DATETIME,
	@AircraftEngineHoursCurrent DECIMAL(11,1) = 0,
	@EngineHoursStart DECIMAL(7,1) = 0,
	@HobbsStart DECIMAL(9,1),
	@AirframeHoursStart DECIMAL(9,1),
	@ReturnTime varchar(8),
	@ReturnDateTime_Local DATETIME,
	@ReturnDateTime_UTC DATETIME,
	@HobbsEnd DECIMAL(9,1),
	@AirframeHoursEnd DECIMAL(9,1),
	@EngineHoursEnd DECIMAL(7,1) = 0,
	@PostFlightFtiOK smallint = 0,
	@PostFlightTelemetryCheckComplete smallint = 0,
	@NacelleCheckBuildup smallint = 0,
	@NacelleCheckDiscoloration smallint = 0,
	@IsApproved BIT = 0,
	@ApprovedBy NVARCHAR(20) = '',
	@UTCDateApproved DATETIME = NULL,
	@FlightGuid uniqueIdentifier,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @IsPreviouslyApproved BIT
	
	IF @IsApproved = 1
	BEGIN
	
		SELECT @IsPreviouslyApproved = IsApproved
		FROM Flight 
		WHERE Flight.FlightId = @FlightId

		IF @IsPreviouslyApproved = 0
		BEGIN
			
			SELECT 
				@ApprovedBy = @StampUserName,
				@UTCDateApproved = GETUTCDATE()
				
		END	
	END
	
	UPDATE [dbo].[Flight]
	SET
		AircraftId = @AircraftId, 
		LocationId = @LocationId, 
		SchedRouteId = @SchedRouteId, 
		FlownRouteId = @FlownRouteId,
		FlightDate = @FlightDate, 
		FlightDateSort = @FlightDateSort,
		WorkShiftIndicatorId = @WorkShiftIndicatorId, 
		AirframeWeightId = @AirframeWeightId,
		Cargo1WeightId = @Cargo1WeightId,
		Cargo2WeightId = @Cargo2WeightId,
		BallastWeightId = @BallastWeightId,
		PilotId = @PilotId,
		PreFlightFtiOK = @PreFlightFtiOK, 
		PreFlightTelemetryOK = @PreFlightTelemetryOK, 
		PreFlightTelemetryCheckComplete = @PreFlightTelemetryCheckComplete, 
		OilLevelIndicatorStart = @OilLevelIndicatorStart, 
		HydraulicLevelIndicatorStart = @HydraulicLevelIndicatorStart, 
		UsedMeterOffset = @UsedMeterOffset, 
		MeterUnitId = @MeterUnitId,
		DepartureTime = @DepartureTime, 
		DepartureDateTime_Local = @DepartureDateTime_Local,
		DepartureDateTime_UTC = @DepartureDateTime_UTC,
		AircraftEngineHoursCurrent = @AircraftEngineHoursCurrent,
		EngineHoursStart = @EngineHoursStart, 
		HobbsStart = @HobbsStart, 
		AirframeHoursStart = @AirframeHoursStart,
		ReturnTime = @ReturnTime, 
		ReturnDateTime_Local = @ReturnDateTime_Local,
		ReturnDateTime_UTC = @ReturnDateTime_UTC,
		HobbsEnd = @HobbsEnd, 
		AirframeHoursEnd = @AirframeHoursEnd,
		EngineHoursEnd = @EngineHoursEnd, 
		PostFlightFtiOK = @PostFlightFtiOK, 
		PostFlightTelemetryCheckComplete = @PostFlightTelemetryCheckComplete, 
		NacelleCheckBuildUp = @NacelleCheckBuildUp, 
		NacelleCheckDiscoloration = @NacelleCheckDiscoloration, 
		IsApproved = @IsApproved,
		ApprovedBy = @ApprovedBy,
		UTCDateApproved = @UTCDateApproved,
		FlightGuid = @FlightGuid, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		FlightId = @FlightId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Flight]
	WHERE 
		FlightId = @FlightId

	UPDATE dbo.Aircraft
		SET 
			Aircraft.StampUserName = @StampUserName,
			Aircraft.UTCDateModified = GETUTCDATE(),
			Aircraft.AirframeHoursCurrent = 
				(SELECT MAX(Flight.AirframeHoursEnd) FROM Flight 
				 WHERE Flight.AircraftId = @AircraftId
				 AND Flight.IsActive = 1 AND Flight.IsDeleted = 0),
			Aircraft.AircraftEngineHoursCurrent = 
				(SELECT MAX(Flight.EngineHoursEnd) FROM Flight
				 WHERE Flight.AircraftId = @AircraftId 
				 AND Flight.IsActive = 1 AND Flight.IsDeleted = 0)
	WHERE dbo.Aircraft.AircraftId = @AircraftId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFlightComment]
	@FlightCommentId int,
	@FlightId Int,
	@ComponentId Int = 0,
	@CommentTime VARCHAR(8),
	@CommentDateTime_UTC DATETIME,
	@CommentDateTime_Local DATETIME,
	@IssueCodeId INT,
	@PilotComment nvarchar(2048),
	@CommentRating SMALLINT,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightComment]
	SET
		FlightId = @FlightId, 
		ComponentId = @ComponentId, 
		CommentTime = @CommentTime, 
		CommentDateTime_UTC = @CommentDateTime_UTC,
		CommentDateTime_Local = @CommentDateTime_Local,
		IssueCodeId = @IssueCodeId,
		PilotComment = @PilotComment, 
		CommentRating = @CommentRating,
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		FlightCommentId = @FlightCommentId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FlightComment]
	WHERE 
		FlightCommentId = @FlightCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFlightEngineRun]
	@FlightEngineRunId int,
	@FlightId Int,
	@RunStartTime VARCHAR(8),
	@RunEndTime VARCHAR(8),
	@EngineSpeed INT,
	@RunStartDateTime_Local DATETIME,
	@RunStartDateTime_UTC DATETIME,
	@RunEndDateTime_Local DATETIME,
	@RunEndDateTime_UTC DATETIME,
	@StampUserName nvarchar(20), 
	@ConcurrencyId timestamp,
	@NewConcurrencyId timestamp OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightEngineRun]
	SET
		FlightId = @FlightId, 
		RunStartTime = @RunStartTime,
		RunEndTime = @RunEndTime, 
		EngineSpeed = @EngineSpeed,
		RunStartDateTime_Local = @RunStartDateTime_Local,
		RunStartDateTime_UTC = @RunStartDateTime_UTC,
		RunEndDateTime_Local = @RunEndDateTime_Local,
		RunEndDateTime_UTC = @RunEndDateTime_UTC,
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		FlightEngineRunId = @FlightEngineRunId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FlightEngineRun]
	WHERE 
		FlightEngineRunId = @FlightEngineRunId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateFlightRegionTime]
	@FlightRegionTimeId int,
	@FlightId Int,
	@RegionId Int,
	@Hours DECIMAL(7,1),
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId timestamp,
	@NewConcurrencyId timestamp OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[FlightRegionTime]
	SET
		FlightId = @FlightId, 
		RegionId = @RegionId, 
		Hours = @Hours, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate()
, 
		StampUserName = @StampUserName 
	WHERE
		FlightRegionTimeId = @FlightRegionTimeId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[FlightRegionTime]
	WHERE 
		FlightRegionTimeId = @FlightRegionTimeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateUTCTimeZone]
	@TimeZoneId INT,
	@TimeZoneCode varchar(10),
	@TimeZoneOffset float,
	@TimeZoneDescr nvarchar(255),
	@DaylightSavingTime BIT,
	@DaylightSavingOffset FLOAT,
	@DaylightSavingBegin DATETIME,
	@DaylightSavingEnd DATETIME,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[UTCTimeZone]
	SET
		TimeZoneCode = @TimeZoneCode,
		TimeZoneOffset = @TimeZoneOffset, 
		TimeZoneDescr = @TimeZoneDescr, 
		DaylightSavingTime = @DaylightSavingTime,
		DaylightSavingOffset = @DaylightSavingOffset,
		DaylightSavingBegin = @DaylightSavingBegin,
		DaylightSavingEnd = @DaylightSavingEnd,
		IsActive = @IsActive  
	WHERE
		TimeZoneId = @TimeZoneId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[UTCTimeZone]
	WHERE 
		TimeZoneId = @TimeZoneId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraft]
	@AircraftId Int,
	@ProgId Int,
	@CurrentLocationId INT,
	@MSN varchar(20),
	@ShipNbr varchar(15),
	@SerialNbr varchar(6),
	@ModelId Int,
	@Description nvarchar(255),
	@EngineerId Int,
	@MechanicId Int,
	@RegistrationNbr varchar(10),
	@LiveryColor varchar(10),
	@AircraftStatusId Int,
	@ManufacturerId Int,
	@AirframeHoursStart DECIMAL(9,1),
	@AirframeHoursCurrent DECIMAL(9,1), 
	@AirframeHoursOffset DECIMAL(9,1),
	@MeterUnitId INT,
	@TotalRequiredUsage DECIMAL(7,1),
	@AircraftEngineHoursCurrent DECIMAL(11,1) = 0,
	@EstEndOfTesting DATETIME,
	@ActEndOfTesting DATETIME,
	@AircraftGuid uniqueIdentifier,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[Aircraft]
	SET
		ProgId = @ProgId, 
		CurrentLocationId = @CurrentLocationid,
		MSN = @MSN, 
		ShipNbr = @ShipNbr, 
		SerialNbr = @SerialNbr, 
		ModelId = @ModelId, 
		Description = @Description, 
		EngineerId = @EngineerId, 
		MechanicId = @MechanicId, 
		RegistrationNbr = @RegistrationNbr, 
		LiveryColor = @LiveryColor, 
		AircraftStatusId = @AircraftStatusId, 
		ManufacturerId = @ManufacturerId, 
		AirframeHoursStart = @AirframeHoursStart, 
		AirframeHoursCurrent = @AirframeHoursCurrent,
		AirframeHoursOffset = @AirframeHoursOffset,
		MeterUnitId = @MeterUnitId,
		TotalRequiredUsage = @TotalRequiredUsage,
		AircraftEngineHoursCurrent = @AircraftEngineHoursCurrent,
		EstEndOfTesting = @EstEndOfTesting,
		ActEndOfTesting = @ActEndOfTesting,
		AircraftGuid = @AircraftGuid, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		AircraftId = @AircraftId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[Aircraft]
	WHERE 
		AircraftId = @AircraftId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraftComment]
	@AircraftCommentId int,
	@AircraftId Int,
	@EngineerId INT,
	@ComponentId INT,
	@CommentDate DATETIME,
	@CommentRating SMALLINT,
	@IssueCodeId INT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftComment]
	SET
		AircraftId = @AircraftId, 
		EngineerId = @EngineerId,
		ComponentId = @ComponentId,
		CommentDate = @CommentDate,
		CommentRating = @CommentRating,
		IssueCodeId = @IssueCodeId,
		Comments = @Comments, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		AircraftCommentId = @AircraftCommentId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[AircraftComment]
	WHERE 
		AircraftCommentId = @AircraftCommentId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraftComponent]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftComponent]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[AircraftComponent]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraftConfiguration]
	@AircraftConfigurationId int,
	@AircraftId Int,
	@ConfigurationDescription nvarchar(255),
	@ConfigurationStartDate datetime,
	@ConfigurationEndDate DATETIME = null,
	@ConfigurationMeterStart DECIMAL(9,1) = 0,
	@ConfigurationMeterEnd DECIMAL(9,1) = 0,
	@ConfigurationMeterOffset DECIMAL(9,1) = 0,
	@MeterUnitId INT,
	@ModelId INT,
	@EngineTypeId Int,
	@EngineEmissionId Int,
	@EngineMaturityId Int,
	@EngineSerialNbr nvarchar(20),
	@APUTypeId Int,
	@APUEmissionId Int,
	@APUMaturityId Int,
	@APUSerialNbr nvarchar(30),
	@NoseGear nvarchar(30),
	@MainGear nvarchar(30),
	@PropellerTypeId Int,
	@PropellerModelNbr nvarchar(20),
	@PropellerSerialNbr nvarchar(20),
	@AvionicsVersion nvarchar(20),
	@EmissionLevel NVARCHAR(20),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftConfiguration]
	SET
		AircraftId = @AircraftId, 
		ConfigurationDescription = @ConfigurationDescription, 
		ConfigurationStartDate = @ConfigurationStartDate, 
		ConfigurationEndDate = @ConfigurationEndDate, 
		ConfigurationMeterStart = @ConfigurationMeterStart, 
		ConfigurationMeterEnd = @ConfigurationMeterEnd, 
		ConfigurationMeterOffset = @ConfigurationMeterOffset,
		MeterUnitId = @MeterUnitId,
		ModelId = @ModelId,
		EngineTypeId = @EngineTypeId,
		EngineEmissionId = @EngineEmissionId,
		EngineMaturityId = @EngineMaturityId,
		EngineSerialNbr = @EngineSerialNbr,
		APUTypeId = @APUTypeId,
		APUEmissionId = @APUEmissionId,
		APUMaturityId = @APUMaturityId,
		APUSerialNbr = @APUSerialNbr,
		NoseGear = @NoseGear,
		MainGear = @MainGear,
		PropellerTypeId = @PropellerTypeId,
		PropellerModelNbr = @PropellerModelNbr,
		PropellerSerialNbr = @PropellerSerialNbr,
		AvionicsVersion = @AvionicsVersion,
		EmissionLevel = @EmissionLevel,
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		AircraftConfigurationId = @AircraftConfigurationId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[AircraftConfiguration]
	WHERE 
		AircraftConfigurationId = @AircraftConfigurationId

END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraftLocation]
	@AircraftLocationId INT,
	@AircraftId Int,
	@LocationId Int,
	@ArrivalDate DATETIME,
	@RequiredUsage DECIMAL(12,1),
	@AccumUsage DECIMAL(12,1),
	@EstEndOfTest DATETIME = null,
	@ActEndOfTest DATETIME = null,
	@LastLocation bit = 0,
	@IsActive bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftLocation]
	SET
		AircraftId = @AircraftId,
		LocationId = @LocationId,
		ArrivalDate = @ArrivalDate,
		RequiredUsage = @RequiredUsage, 
		AccumUsage = @AccumUsage, 
		EstEndOfTest = @EstEndOfTest, 
		ActEndOfTest = @ActEndOfTest, 
		LastLocation = @LastLocation, 
		IsActive = @IsActive , 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
	    AircraftLocationId = @AircraftLocationId AND
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[AircraftLocation]
	WHERE 
	    AircraftLocationId = @AircraftLocationId		

	DECLARE @CurrentLocationId INTEGER
	SELECT @CurrentLocationId = ISNULL(LocationId, 0)
	FROM dbo.AircraftLocation
	WHERE AircraftLocationId = 
		(SELECT MAX(AircraftLocationId)
		 FROM AircraftLocation 
		 WHERE AircraftId = @AircraftId 
		 AND LastLocation = 1
		 AND IsActive = 1
		 AND IsDeleted = 0)
	
	UPDATE dbo.Aircraft
	SET 
		CurrentLocationId = @CurrentLocationId,
		UTCDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE AircraftId = @AircraftId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[updateAircraftStatus]
	@AircraftId varchar(128),
	@Status int, 
	@EngineerComment varchar(128),
	@HangarComment varchar(128)
AS
BEGIN

	if not exists(Select * from [AircraftStatus] where AircraftID = @AircraftID) 
	begin
		insert into AircraftStatus (AircraftID, Status, EngineerComment, HangarComment, UTCDateModified)
		values (@AircraftID, 0, '', '',  GetUTCDate())
	end 

	UPDATE [dbo].[AircraftStatus]
	SET [Status] = @Status,
		[EngineerComment] = @EngineerComment,
		[HangarComment] = @HangarComment,
		[UTCDateModified] =  GetUTCDate()
	WHERE AircraftID = @AircraftID
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateAircraftStatusDef]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[AircraftStatusDef]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[AircraftStatusDef]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWeightDef]
	@WeightDefId int,
	@WeightDefTypeId INT,
	@WeightDefDescr nvarchar(50),
	@Weight INT,
	@WeightUnitId INT,
	@SortOrder Int,
	@IsActive BIT,
	@IsDeleted BIT,
	@StampUserName NVARCHAR(20),
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WeightDef]
	SET
		WeightDefTypeId = @WeightDefTypeId,
		WeightDefDescr = @WeightDefDescr, 
		[Weight] = @Weight,
		WeightUnitId = @WeightUnitId,
		SortOrder = @SortOrder,
		IsActive = @IsActive,
		IsDeleted = @IsDeleted,
		UtcDateModified = GETUTCDATE(),
		StampUserName = @StampUserName
	WHERE
		WeightDefId = @WeightDefId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightDef]
	WHERE 
		WeightDefId = @WeightDefId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWeightDefType]
	@WeightDefTypeId int,
	@WeightDefTypeDescr nvarchar(50),
	@SortOrder Int = 0,
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WeightDefType]
	SET
		WeightDefTypeDescr = @WeightDefTypeDescr, 
		SortOrder = @SortOrder
	WHERE
		WeightDefTypeId = @WeightDefTypeId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightDefType]
	WHERE 
		WeightDefTypeId = @WeightDefTypeId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWeightUnit]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WeightUnit]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WeightUnit]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWOEngineerComment]
	@EngineerCommentsId int,
	@WorkOrderId Int,
	@ComponentId INT,
	@CommentDate DATETIME,
	@CommentRating SMALLINT,
	@IssueCodeId INT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOEngineerComment]
	SET
		WorkOrderId = @WorkOrderId, 
		ComponentId = @ComponentId,
		CommentDate = @CommentDate,
		CommentRating = @CommentRating,
		IssueCodeId = @IssueCodeId,
		Comments = @Comments, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		EngineerCommentsId = @EngineerCommentsId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WOEngineerComment]
	WHERE 
		EngineerCommentsId = @EngineerCommentsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWOInstruction]
	@WOInstructionsId int,
	@WorkOrderId Int,
	@InstructionLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOInstruction]
	SET
		WorkOrderId = @WorkOrderId, 
		InstructionLine = @InstructionLine, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		WOInstructionsId = @WOInstructionsId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WOInstruction]
	WHERE 
		WOInstructionsId = @WOInstructionsId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWOPerformedWork]
	@PerformedWorkId int,
	@WorkOrderId Int,
	@PerformedWorkLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOPerformedWork]
	SET
		WorkOrderId = @WorkOrderId, 
		PerformedWorkLine = @PerformedWorkLine, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		PerformedWorkId = @PerformedWorkId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WOPerformedWork]
	WHERE 
		PerformedWorkId = @PerformedWorkId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWOReason]
	@WOReasonId int,
	@WorkOrderId Int,
	@ReasonLine nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOReason]
	SET
		WorkOrderId = @WorkOrderId, 
		ReasonLine = @ReasonLine, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		WOReasonId = @WOReasonId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WOReason]
	WHERE 
		WOReasonId = @WOReasonId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWorkOrder]
	@WorkOrderId int,
	@ProgId INT,
	@AircraftId Int,
	@LocationId Int,
	@WorkOrderTypeId Int,
	@WorkOrderStatusId INT,
	@EngineerId INT,
	@EngineeringActionId Int,
	@MechanicId Int,
	@RequestDate varchar(10),
	@FaultOccurredDate VARCHAR(10),
	@FaultOccurredMeter DECIMAL(8,1),
	@FISComplaintNbr nvarchar(8),
	@SquawkNbr nvarchar(20),
	@DueDate varchar(10),
	@DueTime varchar(8),
	@MeterStartWork DECIMAL(8,1),
	@FinishDate varchar(10),
	@FinishTime varchar(8),
	@MeterEndWork DECIMAL(8,1) = 0,
	@AircraftDispostion nvarchar(50),
	@AircraftIssueAlert BIT,
	@MaintenanceIssueAlert BIT,
	@IsDeleted bit, 
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrder]
	SET
		AircraftId = @AircraftId, 
		ProgId = @ProgId,
		LocationId = @LocationId, 
		WorkOrderStatusID = @WorkOrderStatusId, 
		WorkOrderTypeId = @WorkOrderTypeId, 
		EngineerId = @EngineerId,
		EngineeringActionId = @EngineeringActionId,
		MechanicId = @MechanicId, 
		RequestDate = @RequestDate, 
		FISComplaintNbr = @FISComplaintNbr, 
		SquawkNbr = @SquawkNbr, 
		FaultOccurredDate = @FaultOccurredDate, 
		FaultOccurredMeter = @FaultOccurredMeter, 
		DueDate = @DueDate, 
		DueTime = @DueTime, 
		MeterStartWork = @MeterStartWork, 
		FinishDate = @FinishDate, 
		FinishTime = @FinishTime, 
		MeterEndWork = @MeterEndWork, 
		AircraftDispostion = @AircraftDispostion, 
		AircraftIssueAlert = @AircraftIssueAlert,
		MaintenanceIssueAlert = @MaintenanceIssueAlert,
		IsDeleted = @IsDeleted, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		WorkOrderId = @WorkOrderId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WorkOrder]
	WHERE 
		WorkOrderId = @WorkOrderId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWorkOrderPart]
	@WorkOrderPartId int,
	@WorkOrderId Int,
	@PartName nvarchar(20),
	@PartDescription nvarchar(255),
	@FisIDRemoved nvarchar(20),
	@PartNbrFailed Int,
	@FisIDAdded nvarchar(20),
	@PartModule nvarchar(20),
	@PartNbrRemoved Int,
	@PartNbrAdded Int,
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrderPart]
	SET
		WorkOrderId = @WorkOrderId, 
		PartName = @PartName, 
		PartDescription = @PartDescription, 
		FisIDRemoved = @FisIDRemoved, 
		PartNbrFailed = @PartNbrFailed, 
		FisIDAdded = @FisIDAdded, 
		PartModule = @PartModule, 
		PartNbrRemoved = @PartNbrRemoved, 
		PartNbrAdded = @PartNbrAdded, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		WorkOrderPartId = @WorkOrderPartId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WorkOrderPart]
	WHERE 
		WorkOrderPartId = @WorkOrderPartId

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWorkOrderStatus]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrderStatus]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkOrderStatus]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWorkOrderType]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkOrderType]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkOrderType]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWorkShift]
	@Id int,
	@ProgId Int,
	@StringValue nvarchar(255),
	@SortOrder Int = 0,
	@IsActive bit, 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WorkShift]
	SET
		ProgId = @ProgId, 
		StringValue = @StringValue, 
		SortOrder = @SortOrder, 
		IsActive = @IsActive  
	WHERE
		Id = @Id AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId
	FROM [dbo].[WorkShift]
	WHERE 
		Id = @Id

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[updateWOMechanicComment]
	@WOMechanicCommentsId int,
	@WorkOrderId Int,
	@ComponentId INT,
	@CommentDate DATETIME,
	@IssueCodeId INT,
	@CommentRating SMALLINT,
	@Comments nvarchar(2048),
	@StampUserName nvarchar(20), 
	@ConcurrencyId bigint,
	@NewConcurrencyId bigint OUTPUT,
	@NewUTCDate datetime OUTPUT
AS
BEGIN
	SET NOCOUNT ON

	UPDATE [dbo].[WOMechanicComment]
	SET
		WorkOrderId = @WorkOrderId, 
		ComponentId = @ComponentId,
		CommentDate = @CommentDate,
		IssueCodeId = @IssueCodeId,
		CommentRating = @CommentRating,
		Comments = @Comments, 
		UTCDateModified = GetUTCDate(), 
		StampUserName = @StampUserName 
	WHERE
		WOMechanicCommentsId = @WOMechanicCommentsId AND 
		ConcurrencyId = @ConcurrencyId

	IF @@ROWCOUNT = 0
		BEGIN
		RAISERROR('Row has been edited by another user', 16, 1)
		RETURN
		END
	
	SELECT
		@NewConcurrencyId = ConcurrencyId,
		@NewUTCDate = UTCDateModified
	FROM [dbo].[WOMechanicComment]
	WHERE 
		WOMechanicCommentsId = @WOMechanicCommentsId

END
GO
USE [master]
GO
ALTER DATABASE [FLT] SET  READ_WRITE 
GO
