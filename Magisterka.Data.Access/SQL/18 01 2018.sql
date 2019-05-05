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
			 FROM [dbo].[StartStopPoints]
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
			 FROM [dbo].[StartStopPoints]
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