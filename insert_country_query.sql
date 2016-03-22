CREATE PROCEDURE InsertCountry(@_CountryName varchar, 
@_ReturnValue int out)

AS
DECLARE @IsCountryExist INT;
 
	SELECT @IsCountryExist = COUNT(*) FROM
       tbl_countrymaster WHERE VC_COUNTRY_NAME = @_CountryName;

	   IF (@IsCountryExist = 0)
		begin
		   INSERT INTO TBL_COUNTRYMASTER(VC_COUNTRY_NAME)
		   values (@_CountryName)
		end
		
		select LAST_VALUE = @_ReturnValue
 Go