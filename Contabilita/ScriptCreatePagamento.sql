USE [Contabilita]
GO

/****** Object:  Table [dbo].[Pagamento]    Script Date: 25/12/2021 11.37.54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Pagamento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Modalita] [varchar](50) NOT NULL,
	[Data] [date] NOT NULL,
	[Importo] [decimal](18, 0) NOT NULL,
	[NumAssegnoBonifico] [nchar](20) NULL,
	[Descrizione] [varchar](250) NULL,
	[Azienda] [nchar](11) NOT NULL,
 CONSTRAINT [PK_Movimento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Pagamento]  WITH CHECK ADD  CONSTRAINT [FK_Movimento_Azienda] FOREIGN KEY([Azienda])
REFERENCES [dbo].[Azienda] ([PartitaIVA])
GO

ALTER TABLE [dbo].[Pagamento] CHECK CONSTRAINT [FK_Movimento_Azienda]
GO


