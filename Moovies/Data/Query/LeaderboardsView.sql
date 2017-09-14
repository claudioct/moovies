SELECT
  ANU.Email UserEmail,
  ANU.UserName UserName,
  LR.Id LeaderboardRecordId,
  LR.UserId ,
  LR.TotalTime ,
  LR.Created ,
  LR.TotalAwards ,
  LR.FavoriteActress ,
  LR.FavoriteActor ,
  LR.FavoriteGenre
FROM `mooviesboarddb`.LeaderboardRecords AS LR
INNER JOIN `mooviesboarddb`.AspNetUsers AS ANU ON ANU.Id = LR.UserId