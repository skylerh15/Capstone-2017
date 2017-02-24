USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_charity')
BEGIN
DROP PROCEDURE sp_create_charity
Print '' print  ' *** dropping procedure sp_create_charity'
End
GO

Print '' print  ' *** creating procedure sp_create_charity'
GO
Create PROCEDURE sp_create_charity
(
@USER_ID[INT],
@EMPLOYEE_ID[INT],
@CHARITY_NAME[NVARCHAR](200),
@CONTACT_FIRST_NAME[NVARCHAR](150),
@CONTACT_LAST_NAME[NVARCHAR](150),
@PHONE_NUMBER[NVARCHAR](20),
@EMAIL[NVARCHAR](100),
@CONTACT_HOURS[NVARCHAR](150)
)
AS
BEGIN
INSERT INTO CHARITY (USER_ID, EMPLOYEE_ID, CHARITY_NAME, CONTACT_FIRST_NAME, CONTACT_LAST_NAME, PHONE_NUMBER, EMAIL, CONTACT_HOURS)
VALUES
(@USER_ID, @EMPLOYEE_ID, @CHARITY_NAME, @CONTACT_FIRST_NAME, @CONTACT_LAST_NAME, @PHONE_NUMBER, @EMAIL, @CONTACT_HOURS)
END
