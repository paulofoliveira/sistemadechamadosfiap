
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/01/2016 13:28:45
-- Generated from EDMX file: D:\SkyDrive\Documentos\Estudos\FIAP\Web e WS\SistemaChamadosFiap\SistemaChamadosFiap.Data\SistemaChamadosFiap.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SistemaChamadosFiap];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_tbChamadoInteracao_tbChamado]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[tbChamadoInteracao] DROP CONSTRAINT [FK_tbChamadoInteracao_tbChamado];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[tbAtendente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbAtendente];
GO
IF OBJECT_ID(N'[dbo].[tbChamado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbChamado];
GO
IF OBJECT_ID(N'[dbo].[tbChamadoInteracao]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbChamadoInteracao];
GO
IF OBJECT_ID(N'[dbo].[tbCliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[tbCliente];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'tbAtendentes'
CREATE TABLE [dbo].[tbAtendentes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(50)  NOT NULL,
    [Ativo] bit  NOT NULL
);
GO

-- Creating table 'tbChamadoes'
CREATE TABLE [dbo].[tbChamadoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titulo] varchar(50)  NOT NULL,
    [Prioridade] tinyint  NOT NULL,
    [Status] tinyint  NOT NULL,
    [DtAbertura] datetime  NOT NULL
);
GO

-- Creating table 'tbClientes'
CREATE TABLE [dbo].[tbClientes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] varchar(50)  NOT NULL,
    [Ativo] bit  NOT NULL
);
GO

-- Creating table 'tbChamadoInteracaos'
CREATE TABLE [dbo].[tbChamadoInteracaos] (
    [Id] int  NOT NULL,
    [Mensagem] nvarchar(max)  NOT NULL,
    [IdChamado] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'tbAtendentes'
ALTER TABLE [dbo].[tbAtendentes]
ADD CONSTRAINT [PK_tbAtendentes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbChamadoes'
ALTER TABLE [dbo].[tbChamadoes]
ADD CONSTRAINT [PK_tbChamadoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'tbClientes'
ALTER TABLE [dbo].[tbClientes]
ADD CONSTRAINT [PK_tbClientes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [Mensagem], [IdChamado] in table 'tbChamadoInteracaos'
ALTER TABLE [dbo].[tbChamadoInteracaos]
ADD CONSTRAINT [PK_tbChamadoInteracaos]
    PRIMARY KEY CLUSTERED ([Id], [Mensagem], [IdChamado] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [IdChamado] in table 'tbChamadoInteracaos'
ALTER TABLE [dbo].[tbChamadoInteracaos]
ADD CONSTRAINT [FK_tbChamadoInteracao_tbChamado]
    FOREIGN KEY ([IdChamado])
    REFERENCES [dbo].[tbChamadoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_tbChamadoInteracao_tbChamado'
CREATE INDEX [IX_FK_tbChamadoInteracao_tbChamado]
ON [dbo].[tbChamadoInteracaos]
    ([IdChamado]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------