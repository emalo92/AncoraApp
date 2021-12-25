USE [Contabilita]
GO

/****** Object:  Table [dbo].[Azienda]    Script Date: 25/12/2021 11.36.34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Azienda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartitaIVA] [nchar](11) NOT NULL,
	[RagioneSociale] [varchar](100) NOT NULL,
	[Telefono] [nchar](20) NULL,
	[Email] [nchar](50) NULL,
	[Iban] [nchar](30) NULL,
 CONSTRAINT [PK_Azienda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
