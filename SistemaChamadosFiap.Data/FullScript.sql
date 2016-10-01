USE [SistemaChamadosFiap]
GO
/****** Object:  Table [dbo].[tbAtendente]    Script Date: 10/1/2016 1:07:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbAtendente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_tbAtendente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbChamado]    Script Date: 10/1/2016 1:07:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbChamado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Prioridade] [tinyint] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[DtAbertura] [datetime] NOT NULL,
 CONSTRAINT [PK_tbChamado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tbChamadoInteracao]    Script Date: 10/1/2016 1:07:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbChamadoInteracao](
	[Id] [int] NOT NULL,
	[Mensagem] [nvarchar](max) NOT NULL,
	[IdChamado] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbCliente]    Script Date: 10/1/2016 1:07:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tbCliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[Ativo] [bit] NOT NULL,
 CONSTRAINT [PK_tbCliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tbAtendente] ADD  CONSTRAINT [DF_tbAtendente_Ativo]  DEFAULT ('1') FOR [Ativo]
GO
ALTER TABLE [dbo].[tbChamado] ADD  CONSTRAINT [DF_tbChamado_Status]  DEFAULT ('0') FOR [Status]
GO
ALTER TABLE [dbo].[tbCliente] ADD  CONSTRAINT [DF_tbCliente_Ativo]  DEFAULT ('1') FOR [Ativo]
GO
ALTER TABLE [dbo].[tbChamadoInteracao]  WITH CHECK ADD  CONSTRAINT [FK_tbChamadoInteracao_tbChamado] FOREIGN KEY([IdChamado])
REFERENCES [dbo].[tbChamado] ([Id])
GO
ALTER TABLE [dbo].[tbChamadoInteracao] CHECK CONSTRAINT [FK_tbChamadoInteracao_tbChamado]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Relacionamento entre interação do chamado e o próprio chamado realizado.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'tbChamadoInteracao', @level2type=N'CONSTRAINT',@level2name=N'FK_tbChamadoInteracao_tbChamado'
GO
