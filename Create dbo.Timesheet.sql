USE [TimesheetMobile]
GO

/****** Object: Table [dbo].[Timesheet] Script Date: 24.2.2019 13.08.54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Timesheet] (
    [Id_Timesheet]      INT     IDENTITY (1, 1)        NOT NULL,
    [Id_Customer]       INT             NULL,
    [Id_Contractor]     INT             NULL,
    [Id_Employee]       INT             NULL,
    [Id_WorkAssignment] INT             NULL,
    [StartTime]         DATETIME        NULL,
    [StopTime]          DATETIME        NULL,
    [Comments]          NVARCHAR (2000) NULL,
    [WorkComplete]      BIT             NULL,
    [CreatedAt]         DATETIME        NULL,
    [LastModifiedAt]    DATETIME        NULL,
    [DeletedAt ]        DATETIME        NULL,
    [Active]            BIT             NULL
);


