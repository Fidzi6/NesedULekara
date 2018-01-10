CREATE PROCEDURE [dbo].[login_user]
      @login VARCHAR(50),
	  @password VARCHAR(50),
	  @rights INT

AS
BEGIN
      SET NOCOUNT ON;
      IF EXISTS(SELECT Id FROM login WHERE login = @login)
      BEGIN
            SELECT -1 -- Username exists.
      END
      ELSE
      BEGIN
            INSERT INTO [login]
                     ([login]
                     ,[password]
                     ,[rights])
            VALUES
                     (@login
                     ,@password
                     ,@rights)
           
            SELECT SCOPE_IDENTITY() -- UserId                 
     END
END