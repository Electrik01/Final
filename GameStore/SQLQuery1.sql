Select Developers.Name,COUNT(Games.Id) as 'Count' From [Games],[Developers]
	Where Developers.Id=Games.Developer_Id
	Group by Developers.Name