USE [SnackOverflowDB]
GO
IF EXISTS(SELECT * FROM sys.objects WHERE type = 'P' AND  name = 'sp_retrieve_message_line_list')
BEGIN
Drop PROCEDURE sp_retrieve_message_line_list
Print '' print  ' *** dropping procedure sp_retrieve_message_line_list'
End
GO

Print '' print  ' *** creating procedure sp_retrieve_message_line_list'
GO
Create PROCEDURE sp_retrieve_message_line_list
AS
BEGIN
SELECT MESSAGE_LINE_ID, DISPATCHER_MESSAGE_ID, MESSAGE_LINE_TEXT
FROM message_line
END
