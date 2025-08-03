namespace prolabcheck.domain.entities
{
    public class tb_productos
    {
        public int NU_ID_PRODUCTO { get; set; }
        public string TXT_NOMBRE_PRODUCTO { get; set; }
        public string TXT_PRODUCTO_CONTENIDO { get; set; }
        public int NU_PRODUCTO_CONTENIDO { get; set; }
        public string TXT_FECHA_PRODUCTO { get; set; }
        public string TXT_PRODUCTO_CODIGO { get; set; }
        public int NUM_CANTIDAD_BUSQUEDA { get; set; }
        public string TX_SITREG { get; set; }
        public string TX_USUREG { get; set; }
        public DateTime? FE_FECHA_REG { get; set; }
        public string TX_USUACT { get; set; }
        public DateTime? FE_FECHA_ACT { get; set; }
        public string TX_USUELI { get; set; }
        public DateTime? FE_FECHA_ELI { get; set; }
    }
}
