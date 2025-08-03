USE [db_aae7e7_prolab]
GO

CREATE PROCEDURE [dbo].[SP_GetProductByInternalCode]
	@InternalCode VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON

	DECLARE @idProduct INT, @countSearch INT

	SET @idProduct = (SELECT TOP 1 NU_ID_PRODUCTO FROM tb_producto_detalle PD WITH(NOLOCK) WHERE PD.TX_DESCRIPCION = @InternalCode)
	SET @countSearch = (SELECT TOP 1 ISNULL(NUM_CANTIDAD_BUSQUEDA, 0) FROM tb_productos WHERE NU_ID_PRODUCTO = @idProduct)

	UPDATE tb_productos
	SET NUM_CANTIDAD_BUSQUEDA = @countSearch + 1
	WHERE NU_ID_PRODUCTO = @idProduct

	SELECT
		P.TXT_NOMBRE_PRODUCTO 'product'
		,P.TXT_PRODUCTO_CONTENIDO 'description'
		,P.NU_PRODUCTO_CONTENIDO content
		,P.TXT_FECHA_PRODUCTO expirationDate
		,PD.TX_DESCRIPCION code
		,P.NUM_CANTIDAD_BUSQUEDA numberOfSearches
	FROM tb_productos P WITH(NOLOCK)
	JOIN tb_producto_detalle PD WITH(NOLOCK) ON P.NU_ID_PRODUCTO = PD.NU_ID_PRODUCTO
	WHERE PD.TX_DESCRIPCION = @InternalCode

	SET NOCOUNT OFF
END