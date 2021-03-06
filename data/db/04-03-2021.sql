USE [voice_dev]
GO
/****** Object:  Table [dbo].[ApplicationLogs]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationLogs](
	[Id] [uniqueidentifier] NOT NULL,
	[Category] [nvarchar](50) NULL,
	[Message] [nvarchar](max) NULL,
	[Details] [nvarchar](max) NULL,
	[Created] [datetime2](7) NULL,
 CONSTRAINT [PK_ApplicationLogs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RandomURL]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RandomURL](
	[Id] [uniqueidentifier] NOT NULL,
	[SourceURL] [nvarchar](max) NOT NULL,
	[ShortURL] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_RandomURL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[DisplayName] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Firstname] [nvarchar](250) NOT NULL,
	[Lastname] [nvarchar](250) NOT NULL,
	[Email] [nvarchar](250) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[ProfileUrl] [nvarchar](100) NULL,
	[FbId] [nvarchar](250) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Active] [bit] NOT NULL,
	[Deleted] [bit] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Modified] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersAccessToken]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersAccessToken](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Purpose] [nvarchar](50) NULL,
	[UniqueId] [uniqueidentifier] NULL,
	[Expired] [bit] NULL,
	[Deleted] [bit] NULL,
	[Active] [bit] NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_UsersAccessToken] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersDevice]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersDevice](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeviceId] [nvarchar](max) NOT NULL,
	[DeviceType] [nvarchar](50) NOT NULL,
	[Created] [datetime] NOT NULL,
 CONSTRAINT [PK_UsersDevice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersRoles]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersRoles](
	[Id] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_UsersRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ApplicationLogs] ADD  CONSTRAINT [DF_ApplicationLogs_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[ApplicationLogs] ADD  CONSTRAINT [DF_ApplicationLogs_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[RandomURL] ADD  CONSTRAINT [DF_RandomURL_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[RandomURL] ADD  CONSTRAINT [DF_RandomURL_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Roles] ADD  CONSTRAINT [DF_Roles_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_EmailConfirmed]  DEFAULT ((0)) FOR [EmailConfirmed]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[UsersAccessToken] ADD  CONSTRAINT [DF_UsersAccessToken_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UsersAccessToken] ADD  CONSTRAINT [DF_UsersAccessToken_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[UsersAccessToken] ADD  CONSTRAINT [DF_UsersAccessToken_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[UsersAccessToken] ADD  CONSTRAINT [DF_UsersAccessToken_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[UsersAccessToken] ADD  CONSTRAINT [DF_UsersAccessToken_Modified]  DEFAULT (getdate()) FOR [Modified]
GO
ALTER TABLE [dbo].[UsersDevice] ADD  CONSTRAINT [DF_UsersDevice_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UsersDevice] ADD  CONSTRAINT [DF_UsersDevice_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[UsersRoles] ADD  CONSTRAINT [DF_UsersRoles_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[UsersAccessToken]  WITH CHECK ADD  CONSTRAINT [FK_UsersAccessToken_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersAccessToken] CHECK CONSTRAINT [FK_UsersAccessToken_Users]
GO
ALTER TABLE [dbo].[UsersDevice]  WITH CHECK ADD  CONSTRAINT [FK_UsersDevice_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersDevice] CHECK CONSTRAINT [FK_UsersDevice_Users]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Roles]
GO
ALTER TABLE [dbo].[UsersRoles]  WITH CHECK ADD  CONSTRAINT [FK_UsersRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[UsersRoles] CHECK CONSTRAINT [FK_UsersRoles_Users]
GO
/****** Object:  StoredProcedure [dbo].[Basic_Structure_For_StoredProcedure]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	Current Date
 Description:	Basic description of API
 Modification Log: 
		Date:	Current Date
		Description: Modification details
 ============================================= */
CREATE PROCEDURE [dbo].[Basic_Structure_For_StoredProcedure]
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;  -- All SP should have this line
	--BEGIN TRANSACTION

		-- Write down your stored procedure code here

	--COMMIT TRANSACTION
END TRY
BEGIN CATCH
	--ROLLBACK TRANSACTION
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Customer_Select_List]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	05-08-2020
 Description:	Select Customer Forms List
 Modification Log: 
		Date:	08/10/2020
		Description: Called user validate SP

 Test:  [dbo].[Customer_Select_List] @PaymentMode ='Cheque' , @UserId = '11AFB2B2-8630-4410-B95B-3BD190F3D758'
 Test:  [dbo].[Customer_Select_List] @PaymentMode ='Online Pay', @UserId = '11AFB2B2-8630-4410-B95B-3BD190F3D758'
 ============================================= */
CREATE PROCEDURE [dbo].[Customer_Select_List]
	@UserId						NVARCHAR(50) = NULL,
	@PaymentMode				NVARCHAR(50) = NULL,
	@SearchParam				NVARCHAR(100) = NULL,
	@PageNo						INT = NULL,
	@PageSize					INT = NULL,
	@OrderBy					NVARCHAR(100) = NULL,
	@UniqueId					NVARCHAR(255) = NULL,
	@DmaStaffUserIdCsv			NVARCHAR(MAX) = NULL,
	@DmaUserIdCsv				NVARCHAR(MAX) = NULL,
	@StartDate				    DATETIME = NULL,
	@EndDate					DATETIME = NULL,
	@GaUserIdCsv				NVARCHAR(MAX) = NULL,
	@CompanyUserIdCsv			NVARCHAR(MAX) = NULL,
	@CityIdCsv					NVARCHAR(MAX) = NULL,
	@AreaIdCsv					NVARCHAR(MAX) = NULL,
	@PincodeIdCsv				NVARCHAR(MAX) = NULL,
	@PremiseTypeIdCsv			NVARCHAR(MAX) = NULL,
	@KichenPointIdCsv			NVARCHAR(MAX) = NULL,
	@GyserPointIdCsv			NVARCHAR(MAX) = NULL,
	@IsLpgGas					BIT = NULL,
	@PaymentTypeCsv				NVARCHAR(50) = NULL,
	@PaymentModeCsv				NVARCHAR(50) = NULL,
	@StatusCsv					NVARCHAR(MAX) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId
	
	-- Fetch Role
	DECLARE @UserRole NVARCHAR(20)
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	SELECT
		C.[id],
		C.[applicationNumber],
		C.[registrationDateTime],
		(C.[Firstname] + ' ' + C.[Surname]) AS [name],
		C.[primaryMobileNumber],
		C.[emailId],
		CI.[Name] AS [cityName],
		S.[Name] AS [streetName],
		C.[pincode],
		C.[amount],
		CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [StateIdCsv] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS stateIdCsv,
		C.[status],
		C.[statusByDma],
		C.[statusByGa],
		(U.[FirstName] + ' ' + U.[LastName]) AS dmaStaff,
		@UserRole AS [role],
		CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [dmaCsv],
		CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [companyCsv],
		CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [gaCsv],
		COUNT(*) OVER () AS [total],
		C.[paymentMode],
		ISNULL(@PageNo,1) AS [offset]
	FROM [dbo].[Customer] C
	LEFT OUTER JOIN [dbo].[City] CI ON C.[CityId] = CI.[Id]
	LEFT OUTER JOIN [dbo].[Street] S ON C.[StreetId] = S.[Id]
	INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
	LEFT OUTER JOIN [dbo].[UsersDma] UD ON C.[DmaStaffId] = UD.[UserId]
	LEFT OUTER JOIN [dbo].[UsersGa] UG ON UG.[UserId] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
	LEFT OUTER JOIN [dbo].[UsersCompany] UC ON UC.[UserId] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))

	WHERE	(@PaymentMode IS NULL OR C.[PaymentMode] = @PaymentMode) 
	AND		(@SearchParam IS NULL OR (
			C.[ApplicationNumber] LIKE '%'+@SearchParam+'%' OR
			C.[PrimaryMobileNumber] LIKE '%'+@SearchParam+'%' OR
			C.[SecondaryMobileNumber] LIKE '%'+@SearchParam+'%' OR 
			C.[EmailId] LIKE '%'+@SearchParam+'%'))

	AND (@DmaStaffUserIdCsv IS NULL OR C.DmaStaffId IN (SELECT value FROM  [dbo].[String_Split](@DmaStaffUserIdCsv, ',')))
	AND (@DmaUserIdCsv IS NULL OR UD.DmaUserIdCsv IN (SELECT value FROM  [dbo].[String_Split](@DmaUserIdCsv, ',')))
	AND (@StartDate IS NULL OR C.RegistrationDateTime >= CONVERT(DATETIME,@StartDate,103))
	AND (@EndDate IS NULL OR C.RegistrationDateTime <= CONVERT(DATETIME,@EndDate,103))
	AND (@GaUserIdCsv IS NULL OR UG.GaUserIdCsv IN (SELECT value FROM  [dbo].[String_Split](@GaUserIdCsv, ',')))
	AND (@CompanyUserIdCsv IS NULL OR UC.CompanyIdCsv IN (SELECT value FROM  [dbo].[String_Split](@CompanyUserIdCsv, ',')))
	AND (@CityIdCsv IS NULL OR C.CityId IN (SELECT value FROM  [dbo].[String_Split](@CityIdCsv, ',')))
	AND (@AreaIdCsv IS NULL OR C.CityId IN (SELECT value FROM  [dbo].[String_Split](@CityIdCsv, ',')))
	AND (@PincodeIdCsv IS NULL OR C.Pincode IN (SELECT value FROM  [dbo].[String_Split](@PincodeIdCsv, ',')))
	AND (@PremiseTypeIdCsv IS NULL OR C.PremiseId IN (SELECT value FROM  [dbo].[String_Split](@PremiseTypeIdCsv, ',')))
	AND (@KichenPointIdCsv IS NULL OR CAST(C.ExtraKitchenPointCount AS NVARCHAR) IN (SELECT value FROM  [dbo].[String_Split](@KichenPointIdCsv, ',')))
	AND (@GyserPointIdCsv IS NULL OR CAST(C.ExtraGyserPointCount AS NVARCHAR) IN (SELECT value FROM  [dbo].[String_Split](@GyserPointIdCsv, ',')))
	AND (@IsLpgGas IS NULL OR C.[IsHavingLpgConnection] = @IsLpgGas)
	AND (@PaymentTypeCsv IS NULL OR C.PaymentType IN (SELECT value FROM  [dbo].[String_Split](@PaymentTypeCsv, ',')))
	AND (@PaymentModeCsv IS NULL OR C.PaymentMode IN (SELECT value FROM  [dbo].[String_Split](@PaymentModeCsv, ',')))
	--AND (@StatusCsv IS NULL OR C.[Status] IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))
	--AND (@StatusCsv IS NULL OR (@UserRole = 'admin' OR @UserRole = 'subadmin' OR @UserRole = 'ga') OR ISNULL(C.[StatusByDma], 'Pending') IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))
	--AND (@StatusCsv IS NULL OR (@UserRole = 'dma' OR @UserRole = 'dmastaff') OR ISNULL(C.[StatusByGa], 'Pending') IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))
	AND (@StatusCsv IS NULL OR (@UserRole = 'admin' OR @UserRole = 'subadmin' OR @UserRole = 'ga') OR ISNULL(C.[StatusByDma], 'Pending') IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))
	AND (@StatusCsv IS NULL OR (@UserRole = 'dma' OR @UserRole = 'dmastaff' OR @UserRole = 'admin' OR @UserRole = 'subadmin') OR ISNULL(C.[StatusByGa], 'Pending') IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))
	AND (@StatusCsv IS NULL OR (@UserRole = 'dma' OR @UserRole = 'dmastaff' OR @UserRole = 'ga') OR ISNULL(C.[Status], 'Pending') IN (SELECT value FROM  [dbo].[String_Split](@StatusCsv, ',')))


	AND (@UserId IS NULL OR 
		 @UserRole = 'admin' OR 
		 @UserRole = 'subadmin' OR
		 (@UserRole  = 'dmastaff' AND C.[DmaStaffId] = @UserId) OR
		 (@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
		 (@UserRole = 'ga' AND  
			(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))
	AND  ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

	ORDER BY  
		CASE WHEN @OrderBy IS NULL OR @OrderBy ='' THEN C.[RegistrationDateTime] END DESC,
		
		CASE WHEN @OrderBy='date_asc' THEN  C.[RegistrationDateTime] END ASC,
		CASE WHEN @OrderBy='date_desc' THEN  C.[RegistrationDateTime]  END DESC,

		CASE WHEN @OrderBy='city_asc' THEN CI.[Name] END ASC,
		CASE WHEN @OrderBy='city_desc' THEN CI.[Name] END DESC,
		
		CASE WHEN @OrderBy='street_asc' THEN S.[Name] END ASC,
		CASE WHEN @OrderBy='street_desc' THEN S.[Name] END DESC,
		
		CASE WHEN @OrderBy='pincode_asc' THEN C.[pincode] END ASC,
		CASE WHEN @OrderBy='pincode_desc' THEN C.[pincode] END DESC,
		
		CASE WHEN @OrderBy='status_asc' THEN C.[status] END ASC,
		CASE WHEN @OrderBy='status_desc' THEN C.[status] END DESC,

		CASE WHEN @OrderBy='dmastaff_asc' THEN (U.[FirstName] + ' ' + U.[LastName]) END ASC,
		CASE WHEN @OrderBy='dmastaff_desc' THEN (U.[FirstName] + ' ' + U.[LastName]) END DESC,
		
		CASE WHEN @OrderBy='dma_asc' THEN (CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
		CASE WHEN @OrderBy='dma_desc' THEN (CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC,
		
		CASE WHEN @OrderBy='ga_asc' THEN (CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
		CASE WHEN @OrderBy='ga_desc' THEN (CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC,

		CASE WHEN @OrderBy='company_asc' THEN (CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
		CASE WHEN @OrderBy='company_desc' THEN (CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC

	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH



--USE [gg_dev_clone]
--GO
--/****** Object:  StoredProcedure [dbo].[Customer_Select_List]    Script Date: 10/12/2020 10:08:33 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--/* =============================================
-- Author:		Akhtarali Ansari
-- Create date:	05-08-2020
-- Description:	Select Customer Forms List
-- Modification Log: 
--		Date:	Current Date
--		Description: Modification details

--		Date:	08/10/2020
--		Description: Called user validate SP

-- Test:  [dbo].[Customer_Select_List] @PaymentMode ='Cheque' , @UserId = '11AFB2B2-8630-4410-B95B-3BD190F3D758'
-- Test:  [dbo].[Customer_Select_List] @PaymentMode ='Online Pay', @UserId = '11AFB2B2-8630-4410-B95B-3BD190F3D758'
-- ============================================= */
--ALTER PROCEDURE [dbo].[Customer_Select_List]
--	@UserId			NVARCHAR(50) = NULL,
--	@PaymentMode	NVARCHAR(50) = NULL,
--	@SearchParam	NVARCHAR(100) = NULL,
--	@PageNo			INT = NULL,
--	@PageSize		INT = NULL,
--	@OrderBy		NVARCHAR(100) = NULL,
--	@UniqueId		NVARCHAR(255) = NULL
--AS
--BEGIN TRY
--	SET NOCOUNT ON; 

--	-- User Validaction.
--	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId
	
--	-- Fetch Role
--	DECLARE @UserRole NVARCHAR(20)
--	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
--	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

--	SELECT
--		C.[id],
--		C.[applicationNumber],
--		C.[registrationDateTime],
--		(C.[Firstname] + ' ' + C.[Surname]) AS [name],
--		C.[primaryMobileNumber],
--		C.[emailId],
--		CI.[Name] AS [cityName],
--		S.[Name] AS [streetName],
--		C.[pincode],
--		C.[amount],
--		CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [StateIdCsv] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS stateIdCsv,
--		C.[status],
--		C.[statusByDma],
--		C.[statusByGa],
--		(U.[FirstName] + ' ' + U.[LastName]) AS dmaStaff,
--		@UserRole AS [role],
--		CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [dmaCsv],
--		CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [companyCsv],
--		CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [gaCsv],
--		COUNT(*) OVER () AS [total],
--		C.[paymentMode],
--		ISNULL(@PageNo,1) AS [offset]
--	FROM [dbo].[Customer] C
--	LEFT OUTER JOIN [dbo].[City] CI ON C.[CityId] = CI.[Id]
--	LEFT OUTER JOIN [dbo].[Street] S ON C.[StreetId] = S.[Id]
--	INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
--	LEFT OUTER JOIN [dbo].[UsersDma] UD ON C.[DmaStaffId] = UD.[UserId]
--	LEFT OUTER JOIN [dbo].[UsersGa] UG ON UG.[UserId] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
--	LEFT OUTER JOIN [dbo].[UsersCompany] UC ON C.[DmaStaffId] = UC.[UserId]

--	WHERE	(@PaymentMode IS NULL OR C.[PaymentMode] = @PaymentMode) 
--	AND		(@SearchParam IS NULL OR (
--			C.[ApplicationNumber] LIKE '%'+@SearchParam+'%' OR
--			C.[PrimaryMobileNumber] LIKE '%'+@SearchParam+'%' OR
--			C.[SecondaryMobileNumber] LIKE '%'+@SearchParam+'%' ))

--	AND (@UserId IS NULL OR 
--		 @UserRole = 'admin' OR 
--		 @UserRole = 'subadmin' OR
--		 (@UserRole  = 'dmastaff' AND C.[DmaStaffId] = @UserId) OR
--		 (@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
--		 (@UserRole = 'ga' AND  
--			(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
--			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))
--	AND  ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

--	ORDER BY  
--		CASE WHEN @OrderBy IS NULL OR @OrderBy ='' THEN C.[RegistrationDateTime] END DESC,
		
--		CASE WHEN @OrderBy='date_asc' THEN  C.[RegistrationDateTime] END ASC,
--		CASE WHEN @OrderBy='date_desc' THEN  C.[RegistrationDateTime]  END DESC,

--		CASE WHEN @OrderBy='city_asc' THEN CI.[Name] END ASC,
--		CASE WHEN @OrderBy='city_desc' THEN CI.[Name] END DESC,
		
--		CASE WHEN @OrderBy='street_asc' THEN S.[Name] END ASC,
--		CASE WHEN @OrderBy='street_desc' THEN S.[Name] END DESC,
		
--		CASE WHEN @OrderBy='pincode_asc' THEN C.[pincode] END ASC,
--		CASE WHEN @OrderBy='pincode_desc' THEN C.[pincode] END DESC,
		
--		CASE WHEN @OrderBy='status_asc' THEN C.[status] END ASC,
--		CASE WHEN @OrderBy='status_desc' THEN C.[status] END DESC,

--		CASE WHEN @OrderBy='dmastaff_asc' THEN (U.[FirstName] + ' ' + U.[LastName]) END ASC,
--		CASE WHEN @OrderBy='dmastaff_desc' THEN (U.[FirstName] + ' ' + U.[LastName]) END DESC,
		
--		CASE WHEN @OrderBy='dma_asc' THEN (CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
--		CASE WHEN @OrderBy='dma_desc' THEN (CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC,
		
--		CASE WHEN @OrderBy='ga_asc' THEN (CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
--		CASE WHEN @OrderBy='ga_desc' THEN (CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [FirstName] + ' ' + [LastName] FROM [dbo].[Users] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UG.[GaUserIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC,

--		CASE WHEN @OrderBy='company_asc' THEN (CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
--		CASE WHEN @OrderBy='company_desc' THEN (CASE WHEN UC.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + [Name] FROM [dbo].[Company] WHERE [Id] IN (SELECT value FROM  [dbo].[String_Split](UC.[CompanyIdCsv], ',')) 
--			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC

--	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
--END TRY
--BEGIN CATCH
	
--	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
--	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
--	DECLARE @ErrorState INT = ERROR_STATE()

--	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
--END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Customer_Update_ContractNumber]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	16-08-2020
 Description:	Update Contract Number of Customer Form as per Application Number
 Modification Log: 

 Test:	[dbo].[Customer_Update_ContractNumber] 'JA01100001', 'Test 1001'
 ============================================= */
CREATE PROCEDURE [dbo].[Customer_Update_ContractNumber]
	@ApplicationNumber		NVARCHAR(50),
	@ContractNumber			NVARCHAR(100),
	@BusinessPartnerId		NVARCHAR(100)
AS
BEGIN TRY
	SET NOCOUNT ON;  

	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Customer] WHERE [ApplicationNumber] = @ApplicationNumber)
	BEGIN
		RAISERROR('sql_error_record_not_found', 15, 1)
		RETURN;
	END

	UPDATE [dbo].[Customer]
	SET
		[ContractNumber]	= COALESCE(@ContractNumber,[ContractNumber]),
		[BusinessPartnerId] = COALESCE(@BusinessPartnerId, [BusinessPartnerId]),
		[Modified]			= GETDATE()	
	WHERE [ApplicationNumber] =  @ApplicationNumber
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Customer_Update_Status]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	19-08-2020
 Description:	Update Customer Forms Status
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
 ============================================= */
CREATE PROCEDURE [dbo].[Customer_Update_Status]
	@Id								NVARCHAR(50),
	@ApplicationNumber				NVARCHAR(50),
	@Status							NVARCHAR(20),
	@Name							NVARCHAR(20),  -- Not used
	@PerformedById					NVARCHAR(50),
	@Remarks						NVARCHAR(MAX) = NULL,
	@StatusByDma					NVARCHAR(20) = NULL,
	@StatusByGa						NVARCHAR(20) = NULL,
	@UniqueId						NVARCHAR(255) = NULL,
	@UserId							NVARCHAR(50) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;  

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId
	
	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Customer] WHERE [Id] = @Id)
	BEGIN
		RAISERROR('sql_error_record_not_found', 15, 1)
		RETURN;
	END

	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Customer] WHERE [Id] = @Id AND [ApplicationNumber] = @ApplicationNumber)
	BEGIN
		RAISERROR('sql_error_application_number_invalid', 15, 1);
		RETURN;
	END

	DECLARE @CustomerSerialized	NVARCHAR(MAX) = NULL
	SELECT TOP(1) @CustomerSerialized = [Serialized] FROM [dbo].[CustomerRevision] WHERE [CustomerId] = @Id AND [ApplicationNumber] = @ApplicationNumber ORDER BY Created DESC

	-- Update Customer 
	UPDATE [dbo].[Customer]
	SET
		[Status]					= COALESCE(@Status, [Status]),
		[StatusByDma]				= COALESCE(@StatusByDma, [StatusByDma]),
		[StatusByGa]				= COALESCE(@StatusByGa, [StatusByGa]),
		[RevisionNumber]			= [RevisionNumber] + 1,	 
		[Modified]					= GETDATE()
	WHERE [Id]  = @Id AND [ApplicationNumber] = @ApplicationNumber

	-- Update Customer Revision
	INSERT INTO [dbo].[CustomerRevision]
	(
		[CustomerId],
		[ApplicationNumber],
		[CurrentRevision],
		[Serialized],
		[PerformedById],
		[Status],
		[Remarks]
	)
	VALUES
	(
		@Id,
		@ApplicationNumber,
		ISNULL((SELECT [RevisionNumber] FROM [dbo].[Customer] WHERE [Id] = @Id AND [ApplicationNumber] = @ApplicationNumber), 0),
		@CustomerSerialized,
		@PerformedById,
		@Status,
		@Remarks
	)

	SELECT TOP(1) @Id AS CustomerId,
		   UD.[DeviceId],
		   UD.[DeviceType],
		   U.[Mobile],
		   (C.[Firstname] + ' ' + C.[Surname]) AS [Name],
		   USD.[DmaUserIdCsv],
		   UG.[GaUserIdCsv] 
	FROM [dbo].[UsersDevice] UD
	LEFT OUTER JOIN [dbo].[Customer] C ON UD.[UserId] = C.[DmaStaffId]	
	LEFT OUTER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
	LEFT OUTER JOIN [dbo].[UsersDma] USD ON C.[DmaStaffId] = USD.[UserId]
	LEFT OUTER JOIN [dbo].[UsersGa] UG ON UG.[UserId] IN (SELECT value FROM  [dbo].[String_Split](USD.[DmaUserIdCsv], ','))
	WHERE C.[Id] = @Id

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Dashboard_Counts_Select]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	24-07-2020
 Description:	Get Dashboard Count
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
 Test: [dbo].[Dashboard_Counts_Select] '11AFB2B2-8630-4410-B95B-3BD190F3D758' 
 ============================================= */
CREATE PROCEDURE [dbo].[Dashboard_Counts_Select]
	@UserId			NVARCHAR(50) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	-- Fetch Role
	DECLARE @Role NVARCHAR(20)
	SELECT @Role = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	DECLARE @TotalRegistrations	INT = 0,
		@LastMonthRegistration	INT = 0,
		@LastWeekRegistration	INT = 0,
		@TodayRegistrations		INT	= 0,
		@ApprovedRegistrations	INT	= 0

	DECLARE	@Ga	INT = 0, @Dma INT = 0,@DmaStaff	INT = 0, @GaManagement INT = 0
	
	IF @Role IS NOT NULL AND (@Role = 'subadmin' OR @Role = 'admin' )
	BEGIN
		SELECT @Ga = COUNT(*) FROM [dbo].[Users] U 
				INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
				INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'ga'

		SELECT @Dma = COUNT(*) FROM [dbo].[Users] U 
				INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
				INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'dma'
				LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId] 

		SELECT @DmaStaff = COUNT(*) FROM [dbo].[Users] U 
				INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
				INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'dmastaff'
				LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId]

		SELECT @TotalRegistrations = COUNT(*) FROM [dbo].[Customer] C
		WHERE  ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @LastMonthRegistration = COUNT(*) FROM [dbo].[Customer] C
		WHERE	DATEPART(m, [RegistrationDateTime]) = DATEPART(m, DATEADD(m, -1, getdate()))
		AND		DATEPART(yyyy, [RegistrationDateTime]) = DATEPART(yyyy, DATEADD(m, -1, getdate()))
		AND		((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
		
		SELECT @LastWeekRegistration = COUNT(*) FROM [dbo].[Customer] C
		 WHERE	[RegistrationDateTime] >= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 7, '17530101') 
		 AND	[RegistrationDateTime] <= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 1, '17530101')
		 AND	((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
		
		SELECT @TodayRegistrations = COUNT(*) FROM [dbo].[Customer] C
		WHERE CAST([RegistrationDateTime] AS DATE)  = CAST(GETDATE() AS Date)
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @ApprovedRegistrations = COUNT(*) FROM [dbo].[Customer] C
		WHERE C.[Status] = 'approved'
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @GaManagement = COUNT(*) FROM [dbo].[GAManagement] WHERE Deleted = 0 AND Active = 1
	END

	IF @Role IS NOT NULL AND (@Role = 'ga')
	BEGIN
		SELECT @Dma = COUNT(*) FROM [dbo].[Users] U 
				INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
				INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'dma'
				LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId] 
			WHERE @UserId IN (SELECT value FROM  [dbo].[String_Split](UG.GaUserIdCsv, ','))

		SELECT @DmaStaff = COUNT(*) FROM [dbo].[Users] U 
				INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
				INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'dmastaff'
				LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId]
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) IN 
			  (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN 
			  (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))

		SELECT @TotalRegistrations = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
	
		SELECT @LastMonthRegistration = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))
		AND DATEPART(m, [RegistrationDateTime]) = DATEPART(m, DATEADD(m, -1, getdate()))
		AND DATEPART(yyyy, [RegistrationDateTime]) = DATEPART(yyyy, DATEADD(m, -1, getdate()))
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
		
		SELECT @LastWeekRegistration = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))
		AND [RegistrationDateTime] >= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 7, '17530101') 
		AND	[RegistrationDateTime] <= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 1, '17530101')
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @TodayRegistrations = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))
		AND CAST([RegistrationDateTime] AS DATE)  = CAST(GETDATE() AS Date)
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @ApprovedRegistrations = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
			IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ',')))
		AND C.[Status] = 'approved'
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
	END

	IF @Role IS NOT NULL AND (@Role = 'dma' )
	BEGIN
		SELECT @DmaStaff = COUNT(*) FROM [dbo].[Users] U 
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = 'dmastaff'
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE @UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))

		SELECT @TotalRegistrations = COUNT(*) FROM [dbo].[Customer] C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE @UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
		
		SELECT @LastMonthRegistration = COUNT(*) FROM [dbo].[Customer] C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE	@UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
		AND		DATEPART(m, [RegistrationDateTime]) = DATEPART(m, DATEADD(m, -1, getdate()))
		AND		DATEPART(yyyy, [RegistrationDateTime]) = DATEPART(yyyy, DATEADD(m, -1, getdate()))
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @LastWeekRegistration = COUNT(*) FROM [dbo].[Customer] C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE	@UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
		AND		[RegistrationDateTime] >= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 7, '17530101') 
		AND		[RegistrationDateTime] <= DATEADD(dd, ((DATEDIFF(dd, '17530101', GETDATE()) / 7) * 7) - 1, '17530101')
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @TodayRegistrations = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE	@UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
		AND		CAST([RegistrationDateTime] AS DATE)  = CAST(GETDATE() AS Date)
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)

		SELECT @ApprovedRegistrations = COUNT(*) FROM [dbo].[Customer]  C
		INNER JOIN [dbo].[Users] U ON C.[DmaStaffId] = U.[Id]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		WHERE	@UserId IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
		AND		C.[Status] = 'approved'
		AND ((SELECT COUNT(*) FROM [dbo].[CustomerRevision] CR WHERE CR.CustomerId = C.Id) > 0)
	END

	SELECT 
		@Ga AS ga,
		@Dma AS dma,
		@DmaStaff AS dmaStaff,
		@TotalRegistrations	AS totalRegistrations,
		@LastMonthRegistration AS lastMonthRegistration,
		@LastWeekRegistration  AS lastWeekRegistration,
		@TodayRegistrations	   AS todayRegistrations,	
		@ApprovedRegistrations AS approvedRegistrations,
		@Role AS [role],
		@GaManagement AS gaaManagement

END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[RandomURL_Persist]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* ============================================
Author:			Uditsing Khati
Create date:	04/11/2020
Description:	SET & UPDATE random URL.
Modification Log: 
		Date:	Current Date
		Description: Modification details
TEST:	
	EXEC [dbo].[RandomURL_Persist]
	@SourceURL	=	N'http://13.232.217.47:9010/',
	@ShortURL	=	N'a692114271ab49f2afc7abc36912dxxx'

	SELECT * FROM [dbo].[RandomURL]
	Id	SourceURL	ShortURL	Created

	[dbo].[RandomURL_Persist] @SourceURL = 'http://13.232.217.47:9020/sendpdf/AR01100001.pdf'
 ============================================= */
CREATE PROCEDURE [dbo].[RandomURL_Persist]
	@SourceURL	NVARCHAR(MAX)	=	NULL--,
	--@ShortURL	NVARCHAR(50)	=	NULL	
AS
BEGIN TRY

	SET NOCOUNT ON;

	-- Check if Source URL is available
	IF EXISTS (SELECT [Id] FROM [dbo].[RandomURL] WHERE [SourceURL] = @SourceURL)
	BEGIN
		SELECT [ShortURL] FROM [dbo].[RandomURL] WHERE [SourceURL] = @SourceURL	
		RETURN;
	END

	DECLARE @ShortUrlKey NVARCHAR(50)

	SET @ShortUrlKey = CAST((NEWID()) AS NVARCHAR(50))
	SET @ShortUrlKey = REPLACE(@ShortUrlKey, N'-', '')
	SET @ShortUrlKey = RIGHT(@ShortUrlKey, 20)

	PRINT(@ShortUrlKey)

	-- Check till Short Url is unique or not
	WHILE EXISTS (SELECT [Id] FROM [dbo].[RandomURL] WHERE [ShortURL] = @ShortUrlKey)
	BEGIN
	    SET @ShortUrlKey = CAST((NEWID()) AS NVARCHAR(50))
		SET @ShortUrlKey = REPLACE(@ShortUrlKey, N'-', '')
		SET @ShortUrlKey = RIGHT(@ShortUrlKey, 20)
	END

	PRINT(@ShortUrlKey)

	INSERT INTO [dbo].[RandomURL]
	(
		[SourceURL],
		[ShortURL]
	)
	VALUES
	(
		@SourceURL,
		@ShortUrlKey--@ShortURL
	)

	-- Select Short URL
	SELECT [ShortURL] FROM [dbo].[RandomURL] WHERE [SourceURL] = @SourceURL	
	
END TRY
BEGIN CATCH

	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);

END CATCH


--USE [gg_dev_clone]
--GO
--/****** Object:  StoredProcedure [dbo].[RandomURL_Persist]    Script Date: 11/5/2020 10:29:58 AM ******/
--SET ANSI_NULLS ON
--GO
--SET QUOTED_IDENTIFIER ON
--GO
--/* ============================================
--Author:			Uditsing Khati
--Create date:	04/11/2020
--Description:	SET & UPDATE random URL.
--Modification Log: 
--		Date:	Current Date
--		Description: Modification details
--TEST:	
--	EXEC [dbo].[RandomURL_Persist]
--	@SourceURL	=	N'http://13.232.217.47:9010/',
--	@ShortURL	=	N'a692114271ab49f2afc7abc36912dxxx'

--	SELECT * FROM [dbo].[RandomURL]
--	Id	SourceURL	ShortURL	Created
-- ============================================= */
--ALTER PROCEDURE [dbo].[RandomURL_Persist]
--	@SourceURL	NVARCHAR(MAX)	=	NULL,
--	@ShortURL	NVARCHAR(50)	=	NULL	
--AS
--BEGIN TRY

--	SET NOCOUNT ON;
	
--	IF NOT EXISTS ( SELECT TOP(1) Id FROM [dbo].[RandomURL] WHERE [SourceURL] = @SourceURL AND [ShortURL] = @ShortURL )
--	BEGIN

--		IF NOT EXISTS ( SELECT TOP(1)  Id FROM [dbo].[RandomURL] WHERE [SourceURL] = @SourceURL )
--		BEGIN

--			IF NOT EXISTS ( SELECT TOP(1)  Id FROM [dbo].[RandomURL] WHERE [ShortURL] = @ShortURL )
--			BEGIN
					
--				INSERT INTO [dbo].[RandomURL]
--				(
--					SourceURL,
--					ShortURL
--				)
--				VALUES
--				(
--					@SourceURL,
--					@ShortURL
--				)
--			END
--			ELSE
--			BEGIN
--				SELECT 1 ;
--			END
--		END
--		ELSE
--		BEGIN
--			IF NOT EXISTS ( SELECT TOP(1)  Id FROM [dbo].[RandomURL] WHERE [ShortURL] = @ShortURL )
--			BEGIN
--				UPDATE [dbo].[RandomURL]
--				SET ShortURL = @ShortURL
--				WHERE SourceURL = @SourceURL
--			END
--			ELSE
--			BEGIN
--				SELECT 1 ;
--			END
--		END
--	END
	
--END TRY
--BEGIN CATCH

--	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
--	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
--	DECLARE @ErrorState INT = ERROR_STATE()

--	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);

--END CATCH
GO
/****** Object:  StoredProcedure [dbo].[RandomURL_Select_List]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =================================================

Author:			Uditsing Khati
Create date:	04/11/2020
Description:	GET random URL.
Modification Log: 
		Date:	Current Date
		Description: Modification details
TEST:	
	EXEC [dbo].[RandomURL_Select_List]
	@ShortURL	=	N'591eb4012899403b8a379f4e41d7b907'

	SELECT * FROM [dbo].[RandomURL]
	Id	SourceURL	ShortURL	Created
 ============================================= */
CREATE PROCEDURE [dbo].[RandomURL_Select_List]
	@ShortURL	NVARCHAR(50)	=	NULL	
AS
BEGIN TRY

	SET NOCOUNT ON;
	
	SELECT [SourceURL] 
	FROM [dbo].[RandomURL] 
	WHERE [ShortURL] = @ShortURL	
	
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Seed_Insert]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author: Akash Limbani
 Create date:	25-02-2021
 Description:	This SP will insert seed data of not available
 Modification Log: 
		Date:		 25-02-2021
		Description: Create SP

Test :  [dbo].[Seed_Insert] 'akash.l@voice.com', 'Voice', 'Admin', '9999988888', 'wKh12Mwde6gUp7nT5wMTZRwNZvikUVeuiVEwzDNd1W0='
 ============================================= */
CREATE PROCEDURE [dbo].[Seed_Insert]
	@EmailId	NVARCHAR (255),
	@Username	NVARCHAR (255) = NULL,
	@Firstname	NVARCHAR (255) = NULL,
	@Lastname	NVARCHAR (255) = NULL,
	@Password	NVARCHAR (255) = NULL
AS
BEGIN TRY
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
		
		-- Add Roles
		IF NOT EXISTS (SELECT * FROM [dbo].[Roles])
		BEGIN
			-- Insert Default Roles
			INSERT INTO [dbo].[Roles] ([Name], [DisplayName])
			VALUES	('admin', 'Admin'),('user','User')
		END

		-- Check And Add Admin In Not Exists
		IF NOT EXISTS (SELECT * FROM [dbo].[Users] WHERE [Email] = @EmailId)
		BEGIN
			
			-- Insert Default Admin
			INSERT INTO [dbo].[Users]
			(
				[Id],
				[Username],
				[Firstname],
				[Lastname],
				[Email],
				[Password],
				[EmailConfirmed]
			)
			VALUES
			(
				NEWID(),
				ISNULL(@Username, 'admin'),
				ISNULL(@Firstname, 'Voice'),
				ISNULL(@Lastname, 'Admin'),
				@EmailId,
				ISNULL(@Password, 'wKh12Mwde6gUp7nT5wMTZRwNZvikUVeuiVEwzDNd1W0='), -- Admin@123
				1
			)

			-- Assign Role to Admin
			INSERT INTO [dbo].[UsersRoles] 
			(
				[UserId],
				[RoleId]
			)
			VALUES
			(
				(SELECT [Id] FROM [dbo].[Users] WHERE [Email] = @EmailId),
				(SELECT [Id] FROM [dbo].[Roles] WHERE [Name] = 'admin')
			)
		END
		
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Insert]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akash Limbani
 Create date:	26-02-2021
 Description:	SP for insert Users record
 Modification Log: 
		

Test : [dbo].[Users_Insert] 'lakash', 'Akash', 'Limbani', 'akash.l@yudiz.in', 'c2BRcpZrimAio7Msir89qV7ebcUzX1xMAtqJaqzXSq4=', '1'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Insert]
	@Username			NVARCHAR(50),
	@Firstname			NVARCHAR(250),
	@Lastname			NVARCHAR(250),
	@EmailId			NVARCHAR(250),
	@Password			NVARCHAR(250),
	@Role				NVARCHAR(50),
	@FbId			NVARCHAR(250),
	@UniqueId			NVARCHAR(255) = NULL,
	@UserId				NVARCHAR(50) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;  

	-- User Validation.
	--EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId, @Username=@Username, @EmailId = @Email
	IF EXISTS (SELECT [Id] FROM Users WHERE [Email] = @EmailId AND [Deleted] = 0 AND [Active] = 1)
		BEGIN
			RAISERROR('sql_error_user_already_exists', 15, 1);
			RETURN;
	END

	DECLARE @UserIdLocal	UNIQUEIDENTIFIER = NEWID()
	DECLARE @UserIdCsv		NVARCHAR(MAX) = ''

	-- Insert in [dbo].[Users]
	INSERT INTO [dbo].[Users]
		(
			[Id],
			[Username],
			[Firstname],
			[Lastname],
			[Email],
			[Password],
			[FbId],
			[ProfileUrl]
		)			
		VALUES
		(
			@UserIdLocal,
			@Username,
			@Firstname,
			@Lastname,
			@EmailId,
			@Password,
			@FbId,
			''
		)

	-- Insert in [dbo].[UsersRoles]
	INSERT INTO [dbo].[UsersRoles]
		(
			[UserId],
			[RoleId]
		)
		VALUES
		(
			@UserIdLocal,
			(SELECT [Id] FROM [dbo].[Roles] WHERE [Name] = @Role)
		)

	SELECT 
		U.[Id],
		U.[Username],
		U.[Firstname],
		U.[Lastname],
		U.[Email],
		U.[Active],
		U.[Deleted],
		U.[Created],
		U.[Modified],	
		U.[FbId],
		R.[Name] AS [Role]
	FROM [dbo].[Users] U
	INNER JOIN [dbo].[UsersRoles] UR ON U.Id = UR.UserId
	INNER JOIN [dbo].[Roles] R ON UR.RoleId = R.Id AND R.[Name] = @Role
	WHERE	U.[Email] = @EmailId
	AND		U.[Password] = @Password
	AND		U.Active = 1
	AND		U.Deleted  = 0

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Password_UpdateReset]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	01-09-2020
 Description:	Update or Reset Password
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP

 Test: [dbo].[Users_Password_UpdateReset] '57B5A904-66D1-47A8-82A6-10DDB7ED8615'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Password_UpdateReset]
	@Id				NVARCHAR(50),
	@UniqueId		NVARCHAR(255) = NULL,
	@UserId			NVARCHAR(50) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;  

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @Id)
	BEGIN
		RAISERROR('sql_error_user_not_found', 15, 1)
		RETURN;
	END

	UPDATE [dbo].[Users] 
	SET		
		[IsPasswordChanged] = 0,
		[Modified] = GETDATE() 
	WHERE Id = @Id

	SELECT 
		U.[FirstName],
		U.[LastName],
		U.[Email],
		U.[Password],
		U.[Mobile]
	FROM [dbo].[Users]  U
	WHERE U.[Id] = @Id

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akash Limbani
 Create date:	26-02-2021
 Description:	Select Login Details
 Modification Log: 
		Date:	Current Date
		Description: Modification details

TEST: 
	[dbo].[Users_Select] @EmailId = 'test.akashl@gmail.comm'
				
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select]
	@UserId			NVARCHAR(50) = NULL,
	@Username			NVARCHAR(50) = NULL,
	@FbId				NVARCHAR(250) = NULL,
	@EmailId			NVARCHAR(250) = NULL,
	@Password			NVARCHAR(250) = NULL,
	@UniqueId			NVARCHAR(255) = NULL,
	@Role				NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	BEGIN
		
		IF(@Password IS NULL)
		BEGIN
			DECLARE @UserIdLocalFB NVARCHAR(50)

			SELECT TOP(1) @UserIdLocalFB = U.[Id] FROM [dbo].[Users] U
			INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
			WHERE [FbId] = @FbId

			DELETE FROM [dbo].[UsersAccessToken] WHERE [UserId] = @UserIdLocalFB
		
			INSERT INTO [dbo].[UsersAccessToken] ([UserId], [UniqueId], [Active]) VALUES (@UserIdLocalFB, @UniqueId, 1);
		END

		IF (@Password IS NOT NULL)
		BEGIN
			DECLARE @UserIdLocal NVARCHAR(50)

			SELECT TOP(1) @UserIdLocal = U.[Id] FROM [dbo].[Users] U
			INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
			WHERE [Username] = @Username OR [FbId] = @FbId

			DELETE FROM [dbo].[UsersAccessToken] WHERE [UserId] = @UserIdLocal
		
			INSERT INTO [dbo].[UsersAccessToken] ([UserId], [UniqueId], [Active]) VALUES (@UserIdLocal, @UniqueId, 1);
		END

		IF @UserId IS NOT NULL
		BEGIN
			IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @UserId)
			BEGIN
				RAISERROR('sql_error_user_not_found', 15, 1)
				RETURN;
			END

			IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @UserId AND [Active] = 1)
			BEGIN
				RAISERROR('sql_error_user_deactivated', 15, 1)
				RETURN;
			END
		END
		ELSE
		BEGIN
			IF(@Username IS NOT NULL)
			BEGIN
				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Username] = @Username)
				BEGIN
					RAISERROR('sql_error_user_not_found', 15, 1)
					RETURN;
				END

				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE ([Username] = @Username AND [Password] = @Password))
				BEGIN
					RAISERROR('sql_error_password_invalid', 15, 1)
					RETURN;
				END

				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Username] = @Username AND [Password] = @Password AND [Active] = 1)
				BEGIN
					RAISERROR('sql_error_user_deactivated', 15, 1)
					RETURN;
				END
			END
			ELSE
			BEGIN
				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [FbId] = @FbId)
				BEGIN
					RAISERROR('sql_error_user_not_found', 15, 1)
					RETURN;
				END

				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [FbId] = @FbId)
				BEGIN
					RAISERROR('sql_error_password_invalid', 15, 1)
					RETURN;
				END

				IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [FbId] = @FbId AND	[Active] = 1)
				BEGIN
					RAISERROR('sql_error_user_deactivated', 15, 1)
					RETURN;
				END
			END
		END

		SELECT 
			U.[Id],
			U.[Username],
			U.[Firstname],
			U.[Lastname],
			U.[Email],
			U.[ProfileUrl],
			U.[Active],
			U.[Deleted],
			U.[Created],
			U.[Modified],
			R.[Name] AS [Role]
		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id]
		WHERE	(@EmailId IS NULL OR U.[Email] = @EmailId) 
		AND		(@UserId IS NULL OR U.Id = @UserId)
		AND		(@Username IS NULL OR U.Username = @Username)
		AND		(@Password IS NULL OR U.[Password] = @Password)
		AND		U.Active = 1
		AND		U.Deleted  = 0
		AND		(@UniqueId IS NULL OR U.[Id] IN (SELECT TOP 1 [UserId] FROM [dbo].[UsersAccessToken] WHERE [UniqueId] = @UniqueId)
		AND		(@Role IS NULL OR R.[Name] = @Role))
	END

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_By_Id]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	15-07-2020
 Description:	Get User Details By Id
 Modification Log: 
		Date:	Current Date
		Description: Modification details 

		Date:	08/10/2020
		Description: Called user validate SP

 TEST: [dbo].[Users_Select_By_Id] '303c9585-7cdc-44d7-b8ca-1cf4496420e6'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_By_Id]
	@Id				NVARCHAR(50),
	@UniqueId		NVARCHAR(255) = NULL,
	@UserId			NVARCHAR(50) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;
	
	-- User Validaction.
	--EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @Id)
	BEGIN
		RAISERROR('sql_error_user_not_found', 15, 1)
		RETURN;
	END

		SELECT 
			U.[Id],
			U.[FirstName],
			U.[LastName],
			U.[Email],
			U.[Mobile],
			U.[ProfileUrl],
			U.[Created],
			U.[Modified],
			UP.[Permissions],
			DP.[DefaultPermission],
			UC.[CityIdCsv],
			U.[DmaCode],
			U.[UserIdForDmaStaff],
			GU.GaIdCsv,
			CASE WHEN UC.[CityIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + City.[Name] FROM [dbo].[City] City  WHERE City.[Id] IN (SELECT value FROM  [dbo].[String_Split](UC.CityIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [CityNamesCsv],
			C.[CompanyIdCsv],
			CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [CompanyNamesCsv],
			UD.[DmaUserIdCsv],
			CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [DmaUsersCsv],
			UG.[GaUserIdCsv],
			CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UG.GaUserIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [GaUsersCsv],
			US.[StreetIdCsv],
			CASE WHEN US.[StreetIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + S.[Name] FROM [dbo].[Street] S WHERE S.[Id] IN (SELECT value FROM  [dbo].[String_Split](US.[StreetIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [StreetNameCsv],
			CASE WHEN GU.[GaIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + (GM.Name) FROM [dbo].[GAManagement] GM  WHERE GM.[Id] IN (SELECT value FROM  [dbo].[String_Split](GU.GaIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [GaNamesCsv]

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		LEFT OUTER JOIN [dbo].[UsersPermissions] UP ON U.[Id] = UP.[UserId] AND UR.[RoleId] = UP.[RoleId]
		LEFT OUTER JOIN [dbo].[UsersCities] UC ON U.[Id] = UC.[UserId]
		LEFT OUTER JOIN [dbo].[UsersCompany] C ON U.[Id] = C.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersStreets] US ON U.[Id] = US.[UserId]
		LEFT OUTER JOIN [dbo].[DefaultPermissions] DP ON UR.[RoleId] = DP.[RoleId]
		LEFT OUTER JOIN [dbo].[GaUsers] GU ON U.[Id] = GU.[UserId]

		WHERE	(@Id IS NULL OR U.Id = @Id)

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_For_Dropdown]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	13-07-2020
 Description:	Get Users List For Dropdown
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
Test : [dbo].[Users_Select_For_Dropdown] @Role = 'dma', @MyRole = 'dma' , @UserId = '33837ED0-3C86-4ED6-9526-165C40EC3485'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_For_Dropdown]
	@Role		NVARCHAR(50)  = NULL,
	@UserId		NVARCHAR(50)  = NULL,
	@MyRole		NVARCHAR(50)  = NULL,
	@UniqueId	NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	IF (@Role IS NULL) SET  @Role = 'ga'
	IF (@MyRole = 'admin' OR @MyRole = 'subadmin') SET @UserId = NULL

	print(@UserId)

	SELECT
		U.[Id] AS [id],
		(U.[FirstName] + ' ' + U.[LastName]) AS [name]
	FROM [dbo].[Users] U 
	INNER JOIN [dbo].[UsersRoles] UR ON U.[Id]  = UR.[UserId]
	INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role

	WHERE	U.[Active]  = 1
	AND		U.[Deleted] = 0
	AND		(@UserId IS NULL OR U.[Id] = @UserId)

	ORDER BY U.[FirstName], U.[LastName]

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorSegment INT = ERROR_State()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorSegment);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_For_List]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	15-07-2020
 Description:	Select Users List
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
Test:
		E5F14AC9-AAD1-4BF6-8A60-8548AAE7463B
 Test: [dbo].[Users_Select_For_List] @Role ='dmastaff' , @UserId = '1C9E86C2-F7DC-4124-B74E-017AA266F097'
 Test: [dbo].[Users_Select_For_List] @Role ='dmastaff' , @UserId = '94622800-781A-4DA7-A907-1DCBDE522686'
 Test: [dbo].[Users_Select_For_List] @Role ='dmastaff' , @UserId = 'E5F14AC9-AAD1-4BF6-8A60-8548AAE7463B'
 Test: [dbo].[Users_Select_For_List] @Role ='dma' , @UserId = 'E5F14AC9-AAD1-4BF6-8A60-8548AAE7463B'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_For_List]
	@UserId			NVARCHAR(50) = NULL,
	@Role			NVARCHAR(50) = NULL,
	@SearchParam	NVARCHAR(100) = NULL,
	@PageNo			INT = NULL,
	@PageSize		INT = NULL,
	@OrderBy		NVARCHAR(100) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DECLARE @UserRole NVARCHAR(50) = NULL
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	;WITH UserList AS 
	(
		SELECT 
		U.[Id] AS [id],
		(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
		U.[Email] AS [email],
		U.[Mobile] AS [mobile],
		U.[Active] AS [active],
		U.[DmaCode] AS [dmaCode],
		U.[UserIdForDmaStaff] AS [userIdForDmaStaff],
		CASE WHEN UC.[CityIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + City.[Name] FROM [dbo].[City] City  WHERE City.[Id] IN (SELECT value FROM  [dbo].[String_Split](UC.CityIdCsv, ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS cityNamesCsv,
		CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UG.GaUserIdCsv, ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS gaUsersCsv,
		CASE WHEN US.[StreetIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + S.[Name] FROM [dbo].[Street] S WHERE S.[Id] IN (SELECT value FROM  [dbo].[String_Split](US.[StreetIdCsv], ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS streetNamesCsv,
		CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS dmaUsersCsv,
		CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS companyNamesCsv,
		COUNT(*) OVER () AS total,
		ISNULL(@PageNo,1) AS  offset

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND (@Role IS NULL OR R.[Name] = @Role)
		LEFT OUTER JOIN [dbo].[UsersCities] UC ON U.[Id] = UC.[UserId]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersStreets] US ON U.[Id] = US.[UserId]
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
		LEFT OUTER JOIN [dbo].[UsersCompany] C ON U.[Id] = C.[UserId]
	
		WHERE	U.[Deleted] = 0
		AND		(@SearchParam IS NULL OR (
				U.[FirstName] LIKE '%'+@SearchParam+'%' OR
				U.[LastName] LIKE '%'+@SearchParam+'%' OR
				U.[Mobile] LIKE '%'+@SearchParam+'%' OR
				U.[Email] LIKE '%'+@SearchParam+'%'))

		AND (@UserId IS NULL OR 
			 @UserRole = 'admin' OR 
			 @UserRole = 'subadmin' OR
			 (@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
			 (@UserRole  = 'ga' AND @Role ='dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UG.[GaUserIdCsv], ','))) OR
			 (@UserRole = 'ga' AND @Role= 'dmastaff' AND  
				(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
				IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))
	)
	
	SELECT * FROM UserList UL
	ORDER BY  
		CASE WHEN @OrderBy IS NULL THEN UL.fullName END ASC,
		CASE WHEN @OrderBy='name_asc' THEN UL.fullName END ASC,
		CASE WHEN @OrderBy='name_desc' THEN UL.fullName END DESC,
		CASE WHEN @OrderBy='email_asc' THEN UL.[Email] END ASC,
		CASE WHEN @OrderBy='email_desc' THEN UL.[Email] END DESC,
		CASE WHEN @OrderBy='mobile_asc' THEN UL.[Mobile] END ASC,
		CASE WHEN @OrderBy='mobile_desc' THEN UL.[Mobile] END DESC,
		CASE WHEN @OrderBy='dma_code_asc' THEN UL.[DmaCode] END ASC,
		CASE WHEN @OrderBy='dma_code_desc' THEN UL.[DmaCode] END DESC,
		CASE WHEN @OrderBy='company_asc' THEN UL.companyNamesCsv END ASC,
		CASE WHEN @OrderBy='company_desc' THEN UL.companyNamesCsv END DESC,
		CASE WHEN @OrderBy='city_asc' THEN UL.cityNamesCsv END ASC,
		CASE WHEN @OrderBy='city_desc' THEN UL.cityNamesCsv END DESC,
		CASE WHEN @OrderBy='ga_asc' THEN UL.gaUsersCsv END ASC,
		CASE WHEN @OrderBy='ga_desc' THEN UL.gaUsersCsv END DESC,
		CASE WHEN @OrderBy='street_asc' THEN UL.streetNamesCsv END ASC,
		CASE WHEN @OrderBy='street_desc' THEN UL.streetNamesCsv END DESC,
		CASE WHEN @OrderBy='dma_asc' THEN UL.dmaUsersCsv END ASC,
		CASE WHEN @OrderBy='dma_desc' THEN UL.dmaUsersCsv END DESC

	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_For_Login]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	07-10-2020
 Description:	Select Login Details

 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_For_Login]
	@EmailId			NVARCHAR(250) = NULL,
	@Password			NVARCHAR(250) = NULL,
	@UniqueId			NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	--EXEC [dbo].[Users_Validate] @UserId = @UserId,@UniqueId=@UniqueId

	-- Validate Email
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Email] = @EmailId)
	BEGIN
		RAISERROR('sql_error_user_not_found', 15, 1)
		RETURN;
	END

	-- Validate Password
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Email] = @EmailId AND [Password] = @Password)
	BEGIN
		RAISERROR('sql_error_password_invalid', 15, 1)
		RETURN;
	END

	-- Validate Is active
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Email] = @EmailId AND [Password] = @Password AND [Active] = 1)
	BEGIN
		RAISERROR('sql_error_user_deactivated', 15, 1)
		RETURN;
	END

	-- Add For Validate User
	IF (@Password IS NOT NULL)
	BEGIN
		DECLARE @UserIdLocal NVARCHAR(50)
		SELECT TOP(1) @UserIdLocal = [Id] FROM [dbo].[Users] WHERE @EmailId IS NULL OR [Email] = @EmailId

		DELETE FROM [dbo].[UsersAccessToken] WHERE [UserId] = @UserIdLocal
		
		INSERT INTO [dbo].[UsersAccessToken] ([UserId], [UniqueId], [Active]) VALUES (@UserIdLocal, @UniqueId, 1);
	END

	SELECT 
		U.[Id],
		U.[FirstName],
		U.[LastName],
		(U.[FirstName] + ' '+ U.[LastName]) AS [Name],
		U.[Email],
		U.[Mobile],
		U.[ProfileUrl],
		U.[Active],
		U.[Deleted],
		U.[Created],
		U.[Modified],
		U.[IsPasswordChanged],
		R.[Name] AS [Role],
		UP.[Permissions],
		DP.[DefaultPermission]
	FROM [dbo].[Users] U
	INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
	INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id]
	LEFT OUTER JOIN [dbo].[UsersPermissions] UP ON U.[Id] = UP.[UserId] AND UR.[RoleId] = UP.[RoleId]
	LEFT OUTER JOIN [dbo].[DefaultPermissions] DP ON UR.[RoleId] = DP.[RoleId]

	WHERE	U.[Email] = @EmailId 
	AND		U.[Password] = @Password
	AND		U.Active = 1
	AND		U.Deleted  = 0

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_For_Login_DmaStaff]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	07-10-2020
 Description:	Select Login Details For DMA Staff

TEST: [dbo].[Users_Select] @UserIdForDmaStaff = 'DMA00001', @Password = 'wKh12Mwde6gUp7nT5wMTZRwNZvikUVeuiVEwzDNd1W0='
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_For_Login_DmaStaff]
	@UserIdForDmaStaff	NVARCHAR(250) = NULL,
	@Password			NVARCHAR(250) = NULL,
	@UniqueId			NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	--EXEC [dbo].[Users_Validate] @UserId = @UserId,@UniqueId=@UniqueId
	
	-- Check UserIdForDmaStaff
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [UserIdForDmaStaff] = @UserIdForDmaStaff)
	BEGIN
		RAISERROR('sql_error_user_not_found', 15, 1)
		RETURN;
	END

	-- Validate Password
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [UserIdForDmaStaff] = @UserIdForDmaStaff AND [Password] = @Password)
	BEGIN
		RAISERROR('sql_error_password_invalid', 15, 1)
		RETURN;
	END

	-- Validate User is Active or not
	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [UserIdForDmaStaff] = @UserIdForDmaStaff AND [Password] = @Password AND [Active] = 1)
	BEGIN
		RAISERROR('sql_error_user_deactivated', 15, 1)
		RETURN;
	END

	-- Add Token for Validate User
	IF (@Password IS NOT NULL)
	BEGIN
		DECLARE @UserIdLocal NVARCHAR(50)
		SELECT TOP(1) @UserIdLocal = [Id] FROM [dbo].[Users] WHERE [UserIdForDmaStaff] = @UserIdForDmaStaff

		DELETE FROM [dbo].[UsersAccessToken] WHERE [UserId] = @UserIdLocal
		
		INSERT INTO [dbo].[UsersAccessToken] ([UserId], [UniqueId], [Active]) VALUES (@UserIdLocal, @UniqueId, 1);
	END

	SELECT 
		U.[Id],
		U.[UserIdForDmaStaff],
		U.[FirstName],
		U.[LastName],
		(U.[FirstName] + ' '+ U.[LastName]) AS [Name],
		U.[Email],
		U.[Mobile],
		U.[ProfileUrl],
		U.[Active],
		U.[Deleted],
		U.[Created],
		U.[Modified],
		U.[IsPasswordChanged],
		R.[Name] AS [Role],
		U.[UserIdForDmaStaff],
		C.[Name] AS CompanyName,
		C.[Logo] AS CompanyLogo
	FROM [dbo].[Users] U
	INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
	INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id]
	LEFT JOIN [dbo].[UsersDma] UD ON UD.UserId = U.Id
	LEFT JOIN [dbo].[UsersCompany] UC ON  UC.[UserId] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
	LEFT JOIN [dbo].[Company] C ON ','+ UC.[CompanyIdCsv] + ',' LIKE '%,'+CAST(C.[Id] AS NVARCHAR(38))+',%'

	WHERE	U.[UserIdForDmaStaff] = @UserIdForDmaStaff
	AND		U.[Password] = @Password
	AND		U.Active = 1
	AND		U.Deleted  = 0

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_Forgot_Password]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	30-06-2020
 Description:	Check email is exists for forgot password
 Modification Log: 
		Date:	Current Date
		Description: Modification details

 TEST:  [dbo].[Users_Select_Forgot_Password] 'akhtarali.a@yudiz.in'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_Forgot_Password]
	@EmailId	NVARCHAR(250)
AS
BEGIN TRY
	SET NOCOUNT ON; 

	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Email] = @EmailId)
	BEGIN
		RAISERROR('sql_error_email_not_found',15,1)
		RETURN;
	END

	SELECT 
		[Id],
		[Email],
		[FirstName],
		[LastName]
	FROM [dbo].[Users] 
	WHERE [Email] = @EmailId

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_List_Dma]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Kunal Vachhani
 Create date:	09-10-2020
 Description:	Select Users List
 Modification Log: 
		Date:	08/10/2020
		Description: Called user validate SP

 Test : [dbo].[Users_Select_List_Dma] @UserId='5ADC8F2F-492B-4795-AADC-E208C8DCBA98'
 Test : [dbo].[Users_Select_List_Dma] @UserId='96e14aef-8485-46d3-9421-a05afaffd22d'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_List_Dma]
	@UserId			NVARCHAR(50) = NULL,
	@SearchParam	NVARCHAR(100) = NULL,
	@PageNo			INT = NULL,
	@PageSize		INT = NULL,
	@OrderBy		NVARCHAR(100) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	DECLARE @Role NVARCHAR(50) = 'dma'

	-- User Validaction.
	--EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DECLARE @UserRole NVARCHAR(50) = NULL
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	IF (@UserRole = 'ga')
	BEGIN
		;WITH DMAList AS(
		SELECT 
			U.[id],
			(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
			U.[email],
			U.[mobile],
			U.[active],
			U.[dmaCode],
			CASE WHEN UC.[CityIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + City.[Name] FROM [dbo].[City] City  WHERE City.[Id] IN (SELECT value FROM  [dbo].[String_Split](UC.CityIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS cityNamesCsv,
			CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UG.GaUserIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS gaUsersCsv,
			CASE WHEN US.[StreetIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + S.[Name] FROM [dbo].[Street] S WHERE S.[Id] IN (SELECT value FROM  [dbo].[String_Split](US.[StreetIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS streetNamesCsv,
			CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS companyNamesCsv,
			COUNT(*) OVER () AS total,
			ISNULL(@PageNo,1) AS  offset

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role
		LEFT OUTER JOIN [dbo].[UsersCities] UC ON U.[Id] = UC.[UserId]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersStreets] US ON U.[Id] = US.[UserId]
		LEFT OUTER JOIN [dbo].[UsersCompany] C ON U.[Id] = C.[UserId]

		WHERE	U.[Deleted] = 0
		AND		(@SearchParam IS NULL OR (
				U.[FirstName] LIKE '%'+@SearchParam+'%' OR
				U.[LastName] LIKE '%'+@SearchParam+'%' OR
				U.[Mobile] LIKE '%'+@SearchParam+'%' OR
				U.[Email] LIKE '%'+@SearchParam+'%'))

		AND (@UserId IS NULL OR @UserId IN (SELECT value FROM [dbo].[String_Split](UG.[GaUserIdCsv], ','))) 
		)
		SELECT * FROM DMAList

		ORDER BY  
			CASE WHEN @OrderBy IS NULL THEN DMAList.[fullName] END ASC,
			CASE WHEN @OrderBy='name_asc' THEN DMAList.[fullName] END ASC,
			CASE WHEN @OrderBy='name_desc' THEN DMAList.[fullName] END DESC,
			CASE WHEN @OrderBy='email_asc' THEN DMAList.[Email] END ASC,
			CASE WHEN @OrderBy='email_desc' THEN DMAList.[Email] END DESC,
			CASE WHEN @OrderBy='mobile_asc' THEN DMAList.[Mobile] END ASC,
			CASE WHEN @OrderBy='mobile_desc' THEN DMAList.[Mobile] END DESC,
			CASE WHEN @OrderBy='dma_code_asc' THEN DMAList.[DmaCode] END ASC,
			CASE WHEN @OrderBy='dma_code_desc' THEN DMAList.[DmaCode] END DESC,
			CASE WHEN @OrderBy='city_asc' THEN DMAList.CityNamesCsv END ASC,
			CASE WHEN @OrderBy='city_desc' THEN DMAList.CityNamesCsv END DESC,
			CASE WHEN @OrderBy='ga_asc' THEN DMAList.gaUsersCsv  END ASC,
			CASE WHEN @OrderBy='ga_desc' THEN DMAList.gaUsersCsv END DESC,
			CASE WHEN @OrderBy='street_asc' THEN DMAList.streetNamesCsv END ASC,
			CASE WHEN @OrderBy='street_desc' THEN DMAList.streetNamesCsv END DESC,
			CASE WHEN @OrderBy='company_asc' THEN DMAList.streetNamesCsv END ASC,
			CASE WHEN @OrderBy='company_desc' THEN DMAList.streetNamesCsv END DESC
			
		OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
	END
	ELSE
	BEGIN
		;WITH DMAList AS(
		SELECT 
			U.[id],
			(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
			U.[email],
			U.[mobile],
			U.[active],
			U.[dmaCode],
			CASE WHEN UC.[CityIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + City.[Name] FROM [dbo].[City] City  WHERE City.[Id] IN (SELECT value FROM  [dbo].[String_Split](UC.CityIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS cityNamesCsv,
			CASE WHEN UG.[GaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UG.GaUserIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS gaUsersCsv,
			CASE WHEN US.[StreetIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + S.[Name] FROM [dbo].[Street] S WHERE S.[Id] IN (SELECT value FROM  [dbo].[String_Split](US.[StreetIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS streetNamesCsv,
			CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS companyNamesCsv,
			COUNT(*) OVER () AS total,
			ISNULL(@PageNo,1) AS  offset

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role
		LEFT OUTER JOIN [dbo].[UsersCities] UC ON U.[Id] = UC.[UserId]
		LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		LEFT OUTER JOIN [dbo].[UsersStreets] US ON U.[Id] = US.[UserId]
		LEFT OUTER JOIN [dbo].[UsersCompany] C ON U.[Id] = C.[UserId]

		WHERE	U.[Deleted] = 0
		AND		(@SearchParam IS NULL OR (
				U.[FirstName] LIKE '%'+@SearchParam+'%' OR
				U.[LastName] LIKE '%'+@SearchParam+'%' OR
				U.[Mobile] LIKE '%'+@SearchParam+'%' OR
				U.[Email] LIKE '%'+@SearchParam+'%'))
		)
		SELECT * FROM DMAList
		
		ORDER BY  
			CASE WHEN @OrderBy IS NULL THEN DMAList.[fullName] END ASC,
			CASE WHEN @OrderBy='name_asc' THEN DMAList.[fullName] END ASC,
			CASE WHEN @OrderBy='name_desc' THEN DMAList.[fullName] END DESC,
			CASE WHEN @OrderBy='email_asc' THEN DMAList.[Email] END ASC,
			CASE WHEN @OrderBy='email_desc' THEN DMAList.[Email] END DESC,
			CASE WHEN @OrderBy='mobile_asc' THEN DMAList.[Mobile] END ASC,
			CASE WHEN @OrderBy='mobile_desc' THEN DMAList.[Mobile] END DESC,
			CASE WHEN @OrderBy='dma_code_asc' THEN DMAList.[DmaCode] END ASC,
			CASE WHEN @OrderBy='dma_code_desc' THEN DMAList.[DmaCode] END DESC,
			CASE WHEN @OrderBy='city_asc' THEN DMAList.CityNamesCsv END ASC,
			CASE WHEN @OrderBy='city_desc' THEN DMAList.CityNamesCsv END DESC,
			CASE WHEN @OrderBy='ga_asc' THEN DMAList.gaUsersCsv  END ASC,
			CASE WHEN @OrderBy='ga_desc' THEN DMAList.gaUsersCsv END DESC,
			CASE WHEN @OrderBy='street_asc' THEN DMAList.streetNamesCsv END ASC,
			CASE WHEN @OrderBy='street_desc' THEN DMAList.streetNamesCsv END DESC,
			CASE WHEN @OrderBy='company_asc' THEN DMAList.streetNamesCsv END ASC,
			CASE WHEN @OrderBy='company_desc' THEN DMAList.streetNamesCsv END DESC
			
		OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
	END
	
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_List_DmaStaff]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Kunal Vachhani
 Create date:	09-10-2020
 Description:	Select Users List
 Modification Log: 
		Date:	08/10/2020
		Description: Called user validate SP
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_List_DmaStaff]
	@UserId			NVARCHAR(50) = NULL,
	@SearchParam	NVARCHAR(100) = NULL,
	@PageNo			INT = NULL,
	@PageSize		INT = NULL,
	@OrderBy		NVARCHAR(100) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
	
	DECLARE @Role NVARCHAR(50) = 'dmastaff'

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DECLARE @UserRole NVARCHAR(50) = NULL
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	;WITH DmaStaff AS 
	(
		SELECT 
		U.[id],
		(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
		U.[email],
		U.[mobile],
		U.[active],
		U.[userIdForDmaStaff],
		CASE WHEN UD.[DmaUserIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + ([User].[FirstName] + ' ' + [User].[LastName]) FROM [dbo].[Users] [User]  WHERE [User].[Id] IN (SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) 
		FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS dmaUsersCsv,
		
		COUNT(*) OVER () AS total,
		ISNULL(@PageNo,1) AS  offset

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role
		LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 
	
		WHERE	U.[Deleted] = 0
		AND		(@SearchParam IS NULL OR (
				U.[FirstName] LIKE '%'+@SearchParam+'%' OR
				U.[LastName] LIKE '%'+@SearchParam+'%' OR
				U.[Mobile] LIKE '%'+@SearchParam+'%' OR
				U.[Email] LIKE '%'+@SearchParam+'%'))

		AND (@UserId IS NULL OR 
			 @UserRole = 'admin' OR 
			 @UserRole = 'subadmin' OR
			 (@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
			 (@UserRole = 'ga' AND 
				--@Role = 'dmastaff' AND  
				(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
				IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))

	)

	SELECT * FROM DmaStaff DSL
	ORDER BY  
		CASE WHEN @OrderBy IS NULL THEN DSL.fullName END ASC,
		CASE WHEN @OrderBy='name_asc' THEN DSL.fullName END ASC,
		CASE WHEN @OrderBy='name_desc' THEN DSL.fullName END DESC,
		CASE WHEN @OrderBy='email_asc' THEN DSL.[Email] END ASC,
		CASE WHEN @OrderBy='email_desc' THEN DSL.[Email] END DESC,
		CASE WHEN @OrderBy='mobile_asc' THEN DSL.[Mobile] END ASC,
		CASE WHEN @OrderBy='mobile_desc' THEN DSL.[Mobile] END DESC,
		--CASE WHEN @OrderBy='company_asc' THEN (CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
		--	FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END ASC,
		--CASE WHEN @OrderBy='company_desc' THEN (CASE WHEN C.[CompanyIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + Com.[Name] FROM [dbo].[Company] Com  WHERE Com.[Id] IN (SELECT value FROM  [dbo].[String_Split](C.[CompanyIdCsv], ',')) 
		--	FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END) END DESC,
		CASE WHEN @OrderBy='dma_asc' THEN DSL.dmaUsersCsv END ASC,
		CASE WHEN @OrderBy='dma_desc' THEN DSL.dmaUsersCsv END DESC

	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_List_Ga]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Kunal Vachhani
 Create date:	09-10-2020
 Description:	Select Users List
 Modification Log: 
		Date:	08/10/2020
		Description: Called user validate SP

 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_List_Ga]
	@UserId			NVARCHAR(50) = NULL,
	@SearchParam	NVARCHAR(100) = NULL,
	@PageNo			INT = NULL,
	@PageSize		INT = NULL,
	@OrderBy		NVARCHAR(100) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
		
	DECLARE @Role NVARCHAR(50) = 'ga'

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DECLARE @UserRole NVARCHAR(50) = NULL
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	;WITH GAList AS 
	(
		SELECT 
			U.[id],
			(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
			U.[email],
			U.[mobile],
			U.[active],
			CASE WHEN UC.[CityIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + City.[Name] FROM [dbo].[City] City  WHERE City.[Id] IN (SELECT value FROM  [dbo].[String_Split](UC.CityIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS cityNamesCsv,
			CASE WHEN GU.[GaIdCsv] != '' THEN STUFF((SELECT DISTINCT ', ' + (GM.Name) FROM [dbo].[GAManagement] GM  WHERE GM.[Id] IN (SELECT value FROM  [dbo].[String_Split](GU.GaIdCsv, ',')) 
			FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'),1,2,'') ELSE '' END AS [gaNamesCsv],
			COUNT(*) OVER () AS total,
			ISNULL(@PageNo,1) AS  offset

		FROM [dbo].[Users] U
		INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
		INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role
		LEFT OUTER JOIN [dbo].[UsersCities] UC ON U.[Id] = UC.[UserId]
		LEFT OUTER JOIN [dbo].[GaUsers] GU ON U.[Id] = GU.[UserId]
		--LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
		--LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 

		WHERE	U.[Deleted] = 0
		AND		(@SearchParam IS NULL OR (
				U.[FirstName] LIKE '%'+@SearchParam+'%' OR
				U.[LastName] LIKE '%'+@SearchParam+'%' OR
				U.[Mobile] LIKE '%'+@SearchParam+'%' OR
				U.[Email] LIKE '%'+@SearchParam+'%'))

		AND (@UserId IS NULL OR 
			 @UserRole = 'admin' OR 
			 @UserRole = 'subadmin')-- OR
			 --(@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
			 --((@UserRole  = 'ga' AND @Role ='dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UG.[GaUserIdCsv], ','))) OR
			 --(@UserRole = 'ga' AND @Role= 'dmastaff' AND  
				--(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
				--IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))

	)

	SELECT * FROM GAList GAL
	ORDER BY  
		CASE WHEN @OrderBy IS NULL THEN GAL.fullName END ASC,
		CASE WHEN @OrderBy='name_asc' THEN GAL.fullName END ASC,
		CASE WHEN @OrderBy='name_desc' THEN GAL.fullName END DESC,
		CASE WHEN @OrderBy='email_asc' THEN GAL.[Email] END ASC,
		CASE WHEN @OrderBy='email_desc' THEN GAL.[Email] END DESC,
		CASE WHEN @OrderBy='mobile_asc' THEN GAL.[Mobile] END ASC,
		CASE WHEN @OrderBy='mobile_desc' THEN GAL.[Mobile] END DESC,
		CASE WHEN @OrderBy='city_asc' THEN GAL.cityNamesCsv END ASC,
		CASE WHEN @OrderBy='city_desc' THEN GAL.cityNamesCsv END DESC

	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Select_List_SubAdmin]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Kunal Vachhani
 Create date:	09-10-2020
 Description:	Select Users List For SubAdmin
 Modification Log: 
		Date:	08/10/2020
		Description: Called user validate SP

 ============================================= */
CREATE PROCEDURE [dbo].[Users_Select_List_SubAdmin] 
	@UserId			NVARCHAR(50) = NULL,
	@SearchParam	NVARCHAR(100) = NULL,
	@PageNo			INT = NULL,
	@PageSize		INT = NULL,
	@OrderBy		NVARCHAR(100) = NULL,
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	DECLARE @Role NVARCHAR(50) = 'subadmin'
	
	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DECLARE @UserRole NVARCHAR(50) = NULL
	SELECT @UserRole = R.[Name] FROM [dbo].[Roles] R 
	INNER JOIN [dbo].[UsersRoles] UR ON R.[Id] = UR.[RoleId] AND UR.[UserId] = @UserId

	;WITH SubAdminList AS (
	SELECT 
		U.[id],
		(U.[FirstName] +' '+ U.[LastName]) AS [fullName],
		U.[email],
		U.[mobile],
		U.[active],
		COUNT(*) OVER () AS total,
		ISNULL(@PageNo,1) AS  offset
	FROM [dbo].[Users] U
	INNER JOIN [dbo].[UsersRoles] UR ON U.[Id] = UR.[UserId]
	INNER JOIN [dbo].[Roles] R ON UR.[RoleId] = R.[Id] AND R.[Name] = @Role
	--LEFT OUTER JOIN [dbo].[UsersGa] UG ON U.[Id] = UG.[UserId]
	--LEFT OUTER JOIN [dbo].[UsersDma] UD ON U.[Id] = UD.[UserId] 

	WHERE	U.[Deleted] = 0
	AND		(@SearchParam IS NULL OR (
			U.[FirstName] LIKE '%'+@SearchParam+'%' OR
			U.[LastName] LIKE '%'+@SearchParam+'%' OR
			U.[Mobile] LIKE '%'+@SearchParam+'%' OR
			U.[Email] LIKE '%'+@SearchParam+'%'))
	)

	--AND (@UserId IS NULL OR 
	--	 @UserRole = 'admin' OR 
	--	 @UserRole = 'subadmin' OR
	--	 (@UserRole  = 'dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UD.[DmaUserIdCsv], ',')) OR 
	--	 (@UserRole  = 'ga' AND @Role ='dma' AND @UserId IN (SELECT value FROM [dbo].[String_Split](UG.[GaUserIdCsv], ','))) OR
	--	 (@UserRole = 'ga' AND @Role= 'dmastaff' AND  
	--		(SELECT value FROM  [dbo].[String_Split](UD.[DmaUserIdCsv], ','))
	--		IN (SELECT UG1.UserId FROM UsersGa UG1 WHERE @UserId IN (SELECT value FROM [dbo].[String_Split](UG1.[GaUserIdCsv], ','))))))

	SELECT * FROM SubAdminList SA
	ORDER BY  
		CASE WHEN @OrderBy IS NULL THEN SA.[fullName] END ASC,
		CASE WHEN @OrderBy='name_asc' THEN SA.fullName END ASC,
		CASE WHEN @OrderBy='name_desc' THEN SA.fullName END DESC,
		CASE WHEN @OrderBy='email_asc' THEN SA.[Email] END ASC,
		CASE WHEN @OrderBy='email_desc' THEN SA.[Email] END DESC,
		CASE WHEN @OrderBy='mobile_asc' THEN SA.[Mobile] END ASC,
		CASE WHEN @OrderBy='mobile_desc' THEN SA.[Mobile] END DESC

	OFFSET ISNULL(@PageSize, 10) * ISNULL(@PageNo, 0) ROWS  FETCH NEXT ISNULL(@PageSize, 10) ROWS ONLY OPTION (RECOMPILE);
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akash Limbani
 Create date:	02-03-2021
 Description:   Update User Details
 Modification Log: 
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Update]
	@Id					NVARCHAR(50),
	@FirstName			NVARCHAR(50) = NULL,
	@LastName			NVARCHAR(50) = NULL,
	@Email				NVARCHAR(50) = NULL,				
	@ProfileUrl			NVARCHAR(100) =	NULL,
	@Active				BIT = NULL,
	@Deleted			BIT = NULL,
	@UniqueId			NVARCHAR(255) = NULL,
	@UserId				NVARCHAR(50) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	IF @Id IS NULL 
	BEGIN
		SET @Id = @UserId;
	END

	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @Id)
	BEGIN
		RAISERROR ('sql_error_user_not_exists', 15, 1)
		RETURN
	END
	IF EXISTS (SELECT [Id] FROM Users WHERE [Email] = @Email AND [Deleted] = 0 AND [Active] = 1 AND [Id] != @Id)
	BEGIN
		RAISERROR('sql_error_email_already_exists', 15, 1);
		RETURN;
	END
	-- Update [dbo].[Users]
	UPDATE [dbo].[Users]
	SET
		[FirstName]			  = COALESCE(@FirstName,[FirstName]),
		[LastName]			  = COALESCE(@LastName,[LastName]),
		[Email]				  = COALESCE(@Email,[Email]), 
		[ProfileUrl]		  = COALESCE(@ProfileUrl,[ProfileUrl]),
		[Active]			  = COALESCE(@Active,[Active]),
		[Deleted]			  = COALESCE(@Deleted,[Deleted]),
		[Modified]			  = GETDATE()
	WHERE [Id]  = @Id
	--END
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Update_Password]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akash Limbani
 Create date:	02-03-2021
 Description:	Used for change password API
 Modification Log: 
		
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Update_Password]
	@Purpose				NVARCHAR(50),
	@UserId					NVARCHAR(50),
	@Password				NVARCHAR(250),
	@CurrentPassword		NVARCHAR(250),
	@UniqueId				NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	--IF(@IsChangePasswordAPI = 1 OR @IsChangePasswordAPI = 'true') 
	--	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	IF NOT EXISTS(SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @UserId AND [Password] = @CurrentPassword)
	AND (@Purpose !='reset_password')
	BEGIN
		RAISERROR('sql_error_provide_valid_old_password', 15, 1);
		RETURN;
	END

	UPDATE [dbo].[Users] 
	SET		
		[Password] = @Password, 
		[EmailConfirmed] = 1, 
		[Active] = 1, 
		[Modified] = GETDATE() 
	WHERE Id = @UserId

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Update_Profile]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	17-07-2020
 Description:   Update Profile Picture
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Update_Profile]
	@Id					NVARCHAR(50),
	@ProfileUrl			NVARCHAR(100) =	NULL,
	@UniqueId			NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @Id, @UniqueId = @UniqueId

	IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @Id)
	BEGIN
		RAISERROR ('sql_error_user_not_exists', 15, 1)
		RETURN
	END

	-- Update [dbo].[Users]
	UPDATE [dbo].[Users]
	SET
		[ProfileUrl]	= @ProfileUrl,
		[Modified]		= GETDATE()
	WHERE [Id]  = @Id

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_Validate]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akash Limbani
 Create date:	01-02-2021
 Description:	Validate User

 Test:  [dbo].[Users_Validate] @UserId ='45C8DA35-4432-494F-BC6A-BAC92904FD93',@UniqueId='2781CBA8-9D69-44B1-A714-A8A5541C2DCB'
 ============================================= */
CREATE PROCEDURE [dbo].[Users_Validate]
	@UserId				NVARCHAR(50) = NULL,
	@UniqueId			NVARCHAR(255) = NULL,
	@Username			NVARCHAR(50) = NULL,
	@EmailId			NVARCHAR(250) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON; 
		
		IF NOT EXISTS (SELECT TOP 1 [UserId] FROM [dbo].[UsersAccessToken] WHERE [UniqueId] = @UniqueId AND [UserId] = @UserId)
		BEGIN
			RAISERROR('error_access_token_expired', 15,1);
			RETURN
		END

		IF NOT EXISTS (SELECT [Id] FROM [dbo].[Users] WHERE [Id] = @UserId AND [Active] = 1)
		BEGIN
			RAISERROR('sql_error_user_deactivated', 15, 1)
			RETURN;
		END

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[UsersAccessToken_Insert]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	24-06-2020
 Description:	Store Access tokens for different purpose
 Modification Log: 
		Date:	Current Date
		Description: Modification details

TEST: UsersAccessToken_Insert '9c55543d-db7c-4e06-a7a5-3bed5d0bd7b6', 'reset_password'  , '053efdd4-2832-4e59-a0d6-06cc99080876'
 ============================================= */
CREATE PROCEDURE [dbo].[UsersAccessToken_Insert]
	@UserId		NVARCHAR(50) = NULL,
	@Purpose	NVARCHAR(50) = NULL,
	@UniqueId	NVARCHAR(50) = NULL,
	@Expired	BIT = 'false'
AS
BEGIN TRY
	SET NOCOUNT ON;  

		IF @UserId IS NOT NULL AND @Purpose IS NOT NULL
		BEGIN
			IF EXISTS (SELECT * FROM [dbo].[UsersAccessToken] WHERE UserId = @UserId AND Purpose = @Purpose AND Expired = 0)
			BEGIN
				UPDATE [dbo].[UsersAccessToken] SET Expired = 1
				WHERE UserId = @UserId AND Purpose = @Purpose
			END

			INSERT INTO [dbo].[UsersAccessToken] 
			(
				[UserId], 
				[Purpose], 
				[UniqueId], 
				[Expired]
			)
			VALUES 
			(
				@UserId, 
				@Purpose, 
				@UniqueId, 
				0
			)
		END

		SELECT 
			[UserId] AS userId, 
			[Purpose] AS purpose, 
			[UniqueId] AS uniqueId, 
			[Expired] AS expired 
		FROM [dbo].[UsersAccessToken] 
		WHERE	UniqueId = @UniqueId 
		AND		Expired = 0 
		AND		UserId = @UserId

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[UsersAccessToken_Update]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	24-06-2020
 Description:	Update Access tokens for different purpose
 Modification Log: 
		Date:	Current Date
		Description: Modification details
 ============================================= */
CREATE PROCEDURE [dbo].[UsersAccessToken_Update]
	@UniqueId	NVARCHAR(50),
	@UserId		NVARCHAR(50)
AS
BEGIN TRY
	SET NOCOUNT ON;  
	BEGIN TRANSACTION

		UPDATE [dbo].[UsersAccessToken] 
		SET Expired = 1
		WHERE UniqueId = @UniqueId AND Expired = 0 AND UserId = @UserId
		
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[UsersDevice_Delete]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	30-07-2020
 Description:	User Device Delete
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
 ============================================= */
CREATE PROCEDURE [dbo].[UsersDevice_Delete]
	@UserId			NVARCHAR(50),
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;  

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DELETE FROM [dbo].[UsersDevice] WHERE [UserId] = @UserId
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[UsersDevice_Insert]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	30-07-2020
 Description:	User Device Insert
 Modification Log: 
		Date:	Current Date
		Description: Modification details

		Date:	08/10/2020
		Description: Called user validate SP
 ============================================= */
CREATE PROCEDURE [dbo].[UsersDevice_Insert]
	@UserId			NVARCHAR(50),
	@DeviceType		NVARCHAR(20),
	@DeviceId		NVARCHAR(MAX),
	@UniqueId		NVARCHAR(255) = NULL
AS
BEGIN TRY
	SET NOCOUNT ON;  

	-- User Validaction.
	EXEC [dbo].[Users_Validate] @UserId = @UserId, @UniqueId = @UniqueId

	DELETE FROM [dbo].[UsersDevice] WHERE [UserId] = @UserId 

	INSERT INTO [dbo].[UsersDevice]
	(
		[UserId],
		[DeviceId],
		[DeviceType]
	)
	VALUES
	(
		@UserId,
		@DeviceId,
		@DeviceType
	)
END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[UsersDevice_Select]    Script Date: 3/4/2021 4:39:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/* =============================================
 Author:		Akhtarali Ansari
 Create date:	30-07-2020
 Description:	User Device Select
 Modification Log: 
		Date:	Current Date
		Description: Modification details
 ============================================= */
CREATE PROCEDURE [dbo].[UsersDevice_Select]
	@UserId		NVARCHAR(50)
AS
BEGIN TRY
	SET NOCOUNT ON;  

	SELECT 
		UD.[Id], 
		UD.[DeviceType],
		UD.[DeviceId]
	FROM [dbo].[UsersDevice] UD

	WHERE UD.[UserId] = @UserId

END TRY
BEGIN CATCH
	
	DECLARE @ErrorMessage NVARCHAR(100) = ERROR_MESSAGE()
	DECLARE @ErrorSeverity INT = ERROR_SEVERITY()
	DECLARE @ErrorState INT = ERROR_STATE()

	RAISERROR(@ErrorMessage, @ErrorSeverity ,@ErrorState);
END CATCH
GO
