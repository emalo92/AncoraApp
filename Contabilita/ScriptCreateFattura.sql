USE [Contabilita]
GO

/****** Object:  Table [dbo].[Fattura]    Script Date: 25/12/2021 11.37.26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Fattura](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [nchar](20) NOT NULL,
	[Data] [date] NOT NULL,
	[Importo] [decimal](18, 0) NOT NULL,
	[Tipo] [nchar](20) NOT NULL,
	[Azienda] [nchar](11) NOT NULL,
 CONSTRAINT [PK_Fattura] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Fattura]  WITH CHECK ADD  CONSTRAINT [FK_Fattura_Azienda] FOREIGN KEY([Azienda])
REFERENCES [dbo].[Azienda] ([PartitaIVA])
GO

ALTER TABLE [dbo].[Fattura] CHECK CONSTRAINT [FK_Fattura_Azienda]
GO


