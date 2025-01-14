USE [projekat]
GO
/****** Object:  Table [dbo].[Korisnik]    Script Date: 6/4/2023 6:16:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Korisnik](
	[idkorisnika] [int] NOT NULL,
	[Ime] [varchar](50) NULL,
	[Prezime] [varchar](50) NULL,
	[Adresa] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idkorisnika] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Narudzbina]    Script Date: 6/4/2023 6:16:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Narudzbina](
	[idnarudzbine] [int] IDENTITY(1,1) NOT NULL,
	[idkorisnika] [int] NULL,
	[ime] [varchar](50) NULL,
	[prezime] [varchar](50) NULL,
	[adresa] [varchar](50) NULL,
	[naziv_proizvoda] [varchar](50) NULL,
	[naziv_brenda] [varchar](50) NULL,
 CONSTRAINT [PK__Narudzbi__EF30155507020F21] PRIMARY KEY CLUSTERED 
(
	[idnarudzbine] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proizvod]    Script Date: 6/4/2023 6:16:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proizvod](
	[idproizvoda] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](100) NULL,
	[Cena] [decimal](10, 2) NULL,
	[Brend] [varchar](50) NULL,
 CONSTRAINT [PK__Proizvod__FF37CD2A03317E3D] PRIMARY KEY CLUSTERED 
(
	[idproizvoda] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Narudzbina]  WITH CHECK ADD  CONSTRAINT [FK__Narudzbin__idkor__08EA5793] FOREIGN KEY([idkorisnika])
REFERENCES [dbo].[Korisnik] ([idkorisnika])
GO
ALTER TABLE [dbo].[Narudzbina] CHECK CONSTRAINT [FK__Narudzbin__idkor__08EA5793]
GO
