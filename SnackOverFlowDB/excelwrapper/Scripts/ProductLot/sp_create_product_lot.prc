USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_create_product_lot')
BEGIN
DROP PROCEDURE sp_create_product_lot
Print '' print  ' *** dropping procedure sp_create_product_lot'
End
GO

Print '' print  ' *** creating procedure sp_create_product_lot'
GO
Create PROCEDURE sp_create_product_lot
(
@WAREHOUSE_ID[INT],
@SUPPLIER_ID[INT],
@LOCATION_ID[INT],
@PRODUCT_ID[INT],
@SUPPLY_MANAGER_ID[INT],
@QUANTITY[INT],
@AVAILABLE_QUANTITY[INT],
@DATE_RECEIVED[DATETIME],
@EXPIRATION_DATE[DATETIME]
)
AS
BEGIN
INSERT INTO PRODUCT_LOT (WAREHOUSE_ID, SUPPLIER_ID, LOCATION_ID, PRODUCT_ID, SUPPLY_MANAGER_ID, QUANTITY, AVAILABLE_QUANTITY, DATE_RECEIVED, EXPIRATION_DATE)
VALUES
(@WAREHOUSE_ID, @SUPPLIER_ID, @LOCATION_ID, @PRODUCT_ID, @SUPPLY_MANAGER_ID, @QUANTITY, @AVAILABLE_QUANTITY, @DATE_RECEIVED, @EXPIRATION_DATE)
END
