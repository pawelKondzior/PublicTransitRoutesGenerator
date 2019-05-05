USE [master]
GO
/****** Object:  Database [Magisterka]    Script Date: 18.01.2018 21:01:30 ******/
CREATE DATABASE [Magisterka]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Magisterka', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Magisterka.mdf' , SIZE = 270336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Magisterka_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Magisterka_log.ldf' , SIZE = 2076352KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Magisterka] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Magisterka].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Magisterka] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Magisterka] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Magisterka] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Magisterka] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Magisterka] SET ARITHABORT OFF 
GO
ALTER DATABASE [Magisterka] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Magisterka] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Magisterka] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Magisterka] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Magisterka] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Magisterka] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Magisterka] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Magisterka] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Magisterka] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Magisterka] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Magisterka] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Magisterka] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Magisterka] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Magisterka] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Magisterka] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Magisterka] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Magisterka] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Magisterka] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Magisterka] SET  MULTI_USER 
GO
ALTER DATABASE [Magisterka] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Magisterka] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Magisterka] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Magisterka] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Magisterka] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Magisterka] SET QUERY_STORE = OFF
GO
USE [Magisterka]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [Magisterka]
GO
/****** Object:  Table [dbo].[Result]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Result](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParametersId] [int] NOT NULL,
	[StartStopPointsId] [bigint] NOT NULL,
	[GenerationTime] [float] NOT NULL,
	[RouteExists] [bit] NOT NULL,
	[Error] [bit] NOT NULL,
 CONSTRAINT [PK_Result] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Parameters]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestAlgorithmTypeEnum] [int] NOT NULL,
	[AdaptationFunctionTypeEnum] [int] NOT NULL,
	[ChangeNumber] [int] NOT NULL,
	[LinkType] [int] NOT NULL,
	[PopulationCount] [int] NOT NULL,
	[MutationProbability] [float] NULL,
	[NumberOfEvaluation] [int] NULL,
	[NumberOfSquares] [int] NULL,
	[NumberOfNeighborSquares] [int] NULL,
	[LockedFor] [uniqueidentifier] NULL,
	[LockDate] [datetime] NULL,
 CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[WorkInProgres]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


/****** Script for SelectTopNRows command from SSMS  *****
SELECT  [Id]
      ,[TestAlgorithmTypeEnum]
      ,[AdaptationFunctionTypeEnum]
      ,[ChangeNumber]
      ,[LinkType]
      ,[PopulationCount]
      ,[MutationProbability]
      ,[NumberOfEvaluation]
      ,[NumberOfSquares]
      ,[NumberOfNeighborSquares]
      ,[LockedFor]
      ,[LockDate]
  FROM [Magisterka].[dbo].[Parameters]*/

CREATE VIEW [dbo].[WorkInProgres]
AS
     SELECT TOP (100) PERCENT COUNT(res.Id) AS ResultNumber,
                              parms.Id,
                              parms.LockedFor,
                              parms.LockDate,
                              parms.TestAlgorithmTypeEnum,
                              parms.AdaptationFunctionTypeEnum,
                              parms.ChangeNumber,
                              parms.LinkType,
                              parms.PopulationCount,
                              parms.MutationProbability,
                              parms.NumberOfEvaluation,
                              parms.NumberOfSquares,
                              parms.NumberOfNeighborSquares
     FROM dbo.Parameters AS parms
          LEFT OUTER JOIN dbo.Result AS res ON parms.Id = res.ParametersId
     GROUP BY parms.Id,
              parms.TestAlgorithmTypeEnum,
              parms.AdaptationFunctionTypeEnum,
              parms.ChangeNumber,
              parms.LinkType,
              parms.PopulationCount,
              parms.MutationProbability,
              parms.NumberOfEvaluation,
              parms.NumberOfSquares,
              parms.NumberOfNeighborSquares,
              parms.LockedFor,
              parms.LockDate
     ORDER BY parms.LockDate desc
              


GO
/****** Object:  Table [dbo].[ErrorLog]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Exception] [nvarchar](max) NULL,
	[StartStopPointId] [int] NULL,
 CONSTRAINT [PK_ErrorLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LastPoints]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LastPoints](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StartPointId] [int] NOT NULL,
	[EndPointId] [int] NOT NULL,
 CONSTRAINT [PK_LastPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Log]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [varchar](255) NOT NULL,
	[Level] [varchar](50) NOT NULL,
	[Logger] [varchar](255) NOT NULL,
	[Message] [varchar](4000) NOT NULL,
	[Exception] [varchar](2000) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SingleResult]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SingleResult](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ResultId] [bigint] NOT NULL,
	[Fitness] [int] NOT NULL,
	[TimeFromStart] [int] NULL,
	[Time] [int] NOT NULL,
	[Parts] [int] NOT NULL,
	[BusConnectionsCount] [int] NOT NULL,
	[WalkConnectionsCount] [int] NOT NULL,
	[LinesCount] [int] NOT NULL,
 CONSTRAINT [PK_SingleResult] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StartStopPoints]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StartStopPoints](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[StartId] [int] NOT NULL,
	[StopId] [int] NOT NULL,
 CONSTRAINT [PK_StartStopPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Parameters] FOREIGN KEY([ParametersId])
REFERENCES [dbo].[Parameters] ([Id])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Parameters]
GO
ALTER TABLE [dbo].[Result]  WITH CHECK ADD  CONSTRAINT [FK_Result_Result] FOREIGN KEY([StartStopPointsId])
REFERENCES [dbo].[StartStopPoints] ([Id])
GO
ALTER TABLE [dbo].[Result] CHECK CONSTRAINT [FK_Result_Result]
GO
ALTER TABLE [dbo].[SingleResult]  WITH CHECK ADD  CONSTRAINT [FK_SingleResult_Result] FOREIGN KEY([ResultId])
REFERENCES [dbo].[Result] ([Id])
GO
ALTER TABLE [dbo].[SingleResult] CHECK CONSTRAINT [FK_SingleResult_Result]
GO
/****** Object:  StoredProcedure [dbo].[ClearDB]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[ClearDB]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
    update dbo.[Parameters] set LockedFor = null, LockDate = null
	delete from dbo.SingleResult
	delete from dbo.Result
	
	--delete from dbo.[Parameters]
	--delete from dbo.StartStopPoints
	
END



GO
/****** Object:  StoredProcedure [dbo].[GetTestIds]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTestIds]
AS
         BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
             SET NOCOUNT ON;


		   WITH Pairs ([Id], [StartId], [StopId])
		  AS
		  -- Define the CTE query.
		  (
			 SELECT ---TOP (1000) 
			 [Id]
		    ,[StartId]
		    ,[StopId]
			 FROM [Magisterka].[dbo].[StartStopPoints]
		  )

		

             SELECT 
		   distinct parm.Id as Id
		   -- parm.Id,
                          
                            --  result.Id AS Expr2,
                            --  result.ParametersId,
                            --  result.StartStopPointsId,
                           --   result.GenerationTime
             FROM dbo.Parameters AS parm
		   CROSS JOIN Pairs AS sp
             --LEFT OUTER JOIN dbo.Result AS result ON result.StartStopPointsId = sp.Id
           --                                               AND result.ParametersId = parm.Id
          --   WHERE(result.Id IS NULL)
           ---      AND parm.TestAlgorithmTypeEnum = 2
        --     ORDER BY parm.TestAlgorithmTypeEnum;


    -- Insert statements for procedure here


         END;

GO
/****** Object:  StoredProcedure [dbo].[GetTestToBeDone]    Script Date: 18.01.2018 21:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO









-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetTestToBeDone] @ParameterId int, @myguid uniqueidentifier 
AS
         BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.


	  WITH Pairs ([Id], [StartId], [StopId])
		  AS
		  -- Define the CTE query.
		  (
			 SELECT TOP (5000) 
			 [Id]
		    ,[StartId]
		    ,[StopId]
			 FROM [Magisterka].[dbo].[StartStopPoints]
		  )
		--  select * from Pairs


             SELECT  top 1000 parm.Id,
                      parm.TestAlgorithmTypeEnum,
                      parm.AdaptationFunctionTypeEnum,
                      parm.ChangeNumber,
                      parm.LinkType,
                      parm.PopulationCount,
                      parm.MutationProbability,
                      parm.NumberOfEvaluation,
                      parm.NumberOfSquares,
                      parm.NumberOfNeighborSquares,
                      sp.Id AS StartStopPointsId,
                      sp.StartId,
                      sp.StopId,
                      result.Id AS Expr2,
                      result.ParametersId,
                      result.StartStopPointsId,
                      result.GenerationTime
     FROM dbo.Parameters AS parm
          CROSS JOIN Pairs AS sp
          LEFT JOIN dbo.Result AS result ON result.StartStopPointsId = sp.Id
                                                  AND result.ParametersId = parm.Id
     WHERE
	parm.id = @ParameterId and parm.[LockedFor] = @myguid and(result.Id IS NULL)
---	parm.id =3694970 --and(result.Id IS NULL)
     ORDER BY parm.TestAlgorithmTypeEnum;


    -- Insert statements for procedure here


         END;









GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "parms"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 306
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "res"
            Begin Extent = 
               Top = 6
               Left = 344
               Bottom = 136
               Right = 540
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WorkInProgres'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'WorkInProgres'
GO
USE [master]
GO
ALTER DATABASE [Magisterka] SET  READ_WRITE 
GO
