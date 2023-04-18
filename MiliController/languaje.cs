using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiliSoftware
{
    public class languaje
    {
        public static dynamic MainWindow;
        public static dynamic PageInventory;
        public static dynamic PageCustomers;
        public static dynamic PageProduct;
        public static dynamic PageProductComponents;
        public static dynamic PageEquivalentProducts;
        public static dynamic PageSuppliers;
        public static dynamic PageSupplier;
        public static dynamic PageEmail;
        public static dynamic PageLogin;
        public static dynamic PageUnitOfMeasurement;

        public static void Init()
        {


            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(
              MiliFileEngine.src.Resources.ResHash.GetHash("SETTING"), Newtonsoft.Json.JsonConvert.SerializeObject(new
              {
                 language = "es-CO",
              //  language = "en-US"
                          }));

            CreateLenjuages();
        }

        private static void CreateLenjuages()
        {
            EnUS();
            EsCO();


            string json = MiliFileEngine.src.Resources.MiliResources.Externs.GetString(MiliFileEngine.src.Resources.ResHash.GetHash("SETTING"));
            dynamic setting = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            string language = setting.language;

            json = MiliFileEngine.src.Resources.MiliResources.Externs.GetString(MiliFileEngine.src.Resources.ResHash.GetHash(language));
            Newtonsoft.Json.Linq.JArray value = (Newtonsoft.Json.Linq.JArray)Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            MainWindow = value[0];
            PageInventory = value[1];
            PageCustomers = value[2];
            PageProduct = value[3];
            PageProductComponents = value[4];
            PageEquivalentProducts = value[5];
            PageSuppliers = value[6];
            PageSupplier = value[7];
            PageEmail = value[8];
            PageLogin = value[9];
            PageUnitOfMeasurement = value[10];
        }

        private static void EnUS() {

            object[] data = new object[10];

            // MainWindow
            data[0] = new
            {
                toolTipTask = "Running a Task",
                toolTipAccount = "Account",
                toolTipHelp = "Help",
                toolTipSetting = "Setting",
                toolTipInventory = "Inventory",
                toolTipCustomers = "Customers",
                toolTipSuppliers = "Suppliers",
                toolTipReports = "Reports",
                toolTipShopping = "Shopping",
                toolTipSales = "Sales",
                toolTipAccounting = "Accounting"
            };

            // Products
            data[1] = new
            {
                hintTbSearch = "Search",
                toolTipBtSearchProduct = "Search product",
                tooTipBtSetting = "Setting",
                tooTipBtAssignGroup = "Assign to a product group",
                toolTipBtUpdate = "Update",
                toolTipBtExport = "Export",
                toolTipBtDelete = "Delete product",
                toolTipBtEdit = "Edit product",
                toolTipBtNew = "New product"
            };

            // Customers
            data[2] = new
            {
                // Stack Panel Tools Controls
                hintTbSearch = "Search",
                toolTipBtSearchProduct = "Search client",
                tooTipBtSetting = "Setting",
                tooTipBtAssignGroup = "Assign to a client group",
                toolTipBtUpdate = "Update",
                toolTipBtExport = "Export",
                toolTipBtDelete = "Delete client",
                toolTipBtEdit = "Edit client",
                toolTipBtNew = "New client",
                //******************************************

                headCode = "Code",
                headNames = "Names",
                headSurnames = "Surnames",
                headId = "ID",
                headMail = "Mail",
                headAddress = "Address"
            };

            // Product
            data[3] = new
            {
                hintTbCode = "Code",
                hintCbType = "Type",
                cbTypesItems = new string[] { "Product", "Service", "Consumption" },
                hintCbGroup = "Inventory group",
                hintTbName = "Name",
                hintTbBarcode = "Barcode",
                hintCbUnitOfMeasurement = "Unit of measurement",
                itemsCbUnitOfMeasurement = new string[] { "Unit", "Centimeter", "Millimeter", "Meter", "Liter", "Kilo", "Gram", "Hour", "Minute" },
                hintTbTaxClassification = "Tax classification",
                itensCbTaxClassification = new string[] { "Excluded", "Exempt", "Taxed" },
                hintTbStore = "Store",
                hintTbPicture = "Photo",
                hintTbDescription = "Product description",
                contentChSaveImage = "Save to server?",
                hintTbKey = "Key",
                hintTbValue = "Value",
                toolTipCbSupplier = "Select Provider",
                hintTbNameSupplier = "Name",
                hintTbPurchaseValue = "Purchase Value",
                hintCbVAT = "VAT",
                hintTbPurchaseValueWithVAT = "Purchase val with VAT",
                hintTbtbWinPercentage = "% profit",
                hintTbSaleValue = "Sale Value",
                contentCbInvoiceWithoutExistence = "Invoice without existence",
                contentChVatIncludedInTheSaleValue = "VAT included in the sale val",
                hintTbCantMin = "Minimum amount",
                contentBtSave = "SAVE",
                contentBtCancel = "CANCEL"
            };

            // Product components
            data[4] = new
            {
                textTbProducts = "Products",
                textTbProductsComponents = "Product components",
                hintTbSearch = "Search",
                toolTipBtSearchProduct = "Search product",
                hintTbCant = "Amount",
                toolTipBtRemove = "Remove product",
                toolTipBtAdd = "Add product",
                toolTipBtExit = "Cancel",
                toolTipBtSave = "Save",
                headCode = "Code",
                headName = "Name",
                headAmount = "Amount"
            };

            // Equivalent Products
            data[5] = new
            {
                textTbProducts = "Products",
                textTbProductsComponents = "Equivalent products",
                hintTbSearch = "Search",
                toolTipBtSearchProduct = "Search product",
                toolTipBtRemove = "Remove product",
                toolTipBtAdd = "Add product",
                toolTipBtExit = "Cancel",
                toolTipBtSave = "Save",
                headCode = "Code",
                headName = "Name",
            };

            // Suppliers
            data[6] = new
            {
                // Stack Panel Tools Controls
                hintTbSearch = "Search",
                toolTipBtSearchProduct = "Search supplier",
                tooTipBtSetting = "Setting",
                tooTipBtAssignGroup = "Assign to a suppliers group",
                toolTipBtUpdate = "Update",
                toolTipBtExport = "Export",
                toolTipBtDelete = "Delete supplier",
                toolTipBtEdit = "Edit supplier",
                toolTipBtNew = "New supplier",
                //******************************************

                headCode = "Code",
                headNames = "Names",
                headSurnames = "Surnames",
                headId = "ID",
                headMail = "Email",
                headAddress = "Address"
            };

            // Supplier
            data[7] = new
            {
                hintTbCode = "Code",
                hintCbCategory = "Category",
                hintCbGrup = "Group",
                hintCbLineOfBusiness = "Line of business",
                hintTbName = "Name",
                hintCbType = "Type",
                hintCbDocumentType = "Document type",
                hintTbDocument = "Document number",
                hintTbPicture = "Supplier photo",
                hintTbDescription = "Provider Description",
                contentChSaveImage = "Save to server?",
                hintTbKey = "Key",
                hintTbValue = "Value",
                hintTbEmail = "Email",
                hintTbCelCode = "(Cod)",
                hintTbContact = "# Contact",
                hintTbBusinessRegistration = "Company registration",
                contentChTaxIncluded = "Tax included",
                hintCbCountry = "Country",
                hintTbCondition = "Department",
                hintTbCity = "City",
                hintTbPostalCode = "Postal code",
                hintTbDirection = "Direction",
                toolTipBtAccountingData = "Accounting Data",
                contentBtSave = "SAVE",
                contentBtCancel = "CANCEL",
            };

            // Email
            data[8] = new
            {
                hintTbTo = "Para",
                hintTbSubject = "Asunto",
                hintTbContent = "Mensaje",
                toolTipBtClose = "Cerrar",
                toolTipBtSend = "Enviar",
                toolTipBtAttached = "Adjuntar",
                contentBtDelete = "Eliminar"
            };

            // Login
            data[9] = new
            {
                contentTbWelcome = "Welcome back!",
                contentTbInfo = "Sign in to an existing account",
                hintTbEmail = "Email",
                hintTbPassword = "Password",
                contentBtnLogin = "LOG IN",
                contentBtnSignup = "Create an account",
                contentBtnHelp = "Help",
                contentBtnExit = "Close",
                toolTipBtnHelp = "Having trouble logging in?",
                toolTipBtnExit = "Exit application"
            };

            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(MiliFileEngine.src.Resources.ResHash.GetHash("en-US"), Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }

        private static void EsCO()
        {

            object[] data = new object[11];

            // MainWindow
            data[0] = new
            {
                toolTipTask = "Ejecutando una tarea",
                toolTipAccount = "Cuenta",
                toolTipHelp = "Ayuda",
                toolTipSetting = "Configuración",
                toolTipInventory = "Inventario",
                toolTipCustomers = "Clientes",
                toolTipSuppliers = "Proveedores",
                toolTipReports = "Reportes",
                toolTipShopping = "Compras",
                toolTipSales = "Ventas",
                toolTipAccounting = "Contabilidad"
            };

            // Products
            data[1] = new
            {
                hintTbSearch = "Buscar",
                toolTipBtSearchProduct = "Buscar producto",
                tooTipBtSetting = "Ajustes",
                tooTipBtAssignGroup = "Asignar a un grupo de productos",
                toolTipBtUpdate = "Actualizar",
                toolTipBtExport = "Exportar",
                toolTipBtDelete = "Eliminar producto",
                toolTipBtEdit = "Editar producto",
                toolTipBtNew = "Nuevo producto"
            };

            // Customers
            data[2] = new
            {
                // Stack Panel Tools Controls
                hintTbSearch = "Buscar",
                toolTipBtSearchProduct = "Buscar cliente",
                tooTipBtSetting = "Ajustes",
                tooTipBtAssignGroup = "Asignar a un grupo de clientes",
                toolTipBtUpdate = "Actualizar",
                toolTipBtExport = "Exportar",
                toolTipBtDelete = "Eliminar cliente",
                toolTipBtEdit = "Editar cliente",
                toolTipBtNew = "Nuevo cliente",
                //******************************************

                headCode = "Código",
                headNames = "Nombres",
                headSurnames = "Apellidos",
                headId = "Identificación",
                headMail = "Correo",
                headAddress = "Dirección"
            };

            // Product
            data[3] = new
            {
                hintTbCode = "Código",
                hintCbType = "Tipo",
                cbTypesItems = new string[] { "Producto", "Servicio", "Consumo" },
                hintCbGroup = "Grupo de inventario",
                hintTbName = "Nombre",
                hintTbBarcode = "Código de barras",
                hintCbUnitOfMeasurement = "Unidad de medida",
                itemsCbUnitOfMeasurement = new string[] { "Unidad", "Centímetro", "Milimetro", "Metro", "Litro", "Kilo", "Gramo", "Hora", "Minuto" },
                hintCbTaxClassification = "Clasificación tributaria",
                itensCbTaxClassification = new string[] { "Excluido", "Exento", "Gravado" },
                hintTbStore = "Bodega",
                hintTbPicture = "Foto",
                hintTbDescription = "Descripción del producto",
                contentChSaveImage = "Guardar en el servidor?",
                hintTbKey = "Llave",
                hintTbValue = "Valor",
                toolTipCbSupplier = "Selecionar Proveedor",
                hintTbNameSupplier = "Nombre",
                hintTbPurchaseValue = "Vr. de Compra",
                hintCbVAT = "IMP",
                hintTbPurchaseValueWithVAT = "Vr. de Compra con IMP" +
                "",
                hintTbtbWinPercentage = "% de ganancia",
                hintTbSaleValue = "Vr. de Venta",
                contentCbInvoiceWithoutExistence = "Facturar sin existencia",
                contentChVatIncludedInTheSaleValue = "IVA incluido al Vr. de venta",
                hintTbCantMin = "Cantidad Mínima",
                contentBtSave = "GUARDAR",
                contentBtCancel = "CANCELAR"
            };

            // Product components
            data[4] = new
            {
                textTbProducts = "Productos",
                textTbProductsComponents = "Componentes del producto",
                hintTbSearch = "Buscar",
                toolTipBtSearchProduct = "Buscar producto",
                hintTbCant = "Cant",
                toolTipBtRemove = "Remover producto",
                toolTipBtAdd = "Agregar producto",
                toolTipBtExit = "Cancelar",
                toolTipBtSave = "Guardar",
                headCode = "Código",
                headName = "Nombre",
                headAmount = "Cantidad"
            };

            // Equivalent Products
            data[5] = new
            {
                textTbProducts = "Productos",
                textTbProductsComponents = "Productos equivalentes",
                hintTbSearch = "Buscar",
                toolTipBtSearchProduct = "Buscar producto",
                toolTipBtRemove = "Remover producto",
                toolTipBtAdd = "Agregar producto",
                toolTipBtExit = "Cancelar",
                toolTipBtSave = "Guardar",
                headCode = "Código",
                headName = "Nombre",
            };

            // Suppliers
            data[6] = new
            {
                // Stack Panel Tools Controls
                hintTbSearch = "Buscar",
                toolTipBtSearchProduct = "Buscar proveedor",
                tooTipBtSetting = "Ajustes",
                tooTipBtAssignGroup = "Asignar a un grupo de proveedores",
                toolTipBtUpdate = "Actualizar",
                toolTipBtExport = "Exportar",
                toolTipBtDelete = "Eliminar proveedor",
                toolTipBtEdit = "Editar proveedor",
                toolTipBtNew = "Nuevo proveedor",
                //******************************************

                headCode = "Código",
                headNames = "Nombres",
                headSurnames = "Apellidos",
                headId = "Identificación",
                headMail = "Correo",
                headAddress = "Dirección"
            };

            // Supplier
            data[7] = new
            {
                hintTbCode = "Código",
                hintCbCategory = "Categoría",
                hintCbGrup = "Grupo",
                hintCbLineOfBusiness = "Giro del negocio",
                hintTbName = "Nombre",
                hintCbType = "Tipo",
                hintCbDocumentType = "Tipo de documento",
                hintTbDocument = "Número de documento",
                hintTbPicture = "Foto del proveedor",
                hintTbDescription = "Descripción del proveedor",
                contentChSaveImage = "Guardar en el servidor?",
                hintTbKey = "Llave",
                hintTbValue = "Valor",
                hintTbEmail = "Correo Electrónico",
                hintTbCelCode = "(Cod)",
                hintTbContact = "# Contacto",
                hintTbBusinessRegistration = "Registro de empresa",
                contentChTaxIncluded = "Impuesto Incluido",
                hintCbCountry = "País",
                hintTbCondition = "Departamento",
                hintTbCity = "Ciudad",
                hintTbPostalCode = "Código postal",
                hintTbDirection = "Dirección",
                toolTipBtAccountingData = "Datos Contables",
                contentBtSave = "GUARDAR",
                contentBtCancel = "CANCELAR",

                cbTypesItems = new string[] { "Producto", "Servicio", "Consumo" },
                hintCbGroup = "Grupo de inventario",
                hintTbBarcode = "Código de barras",
                hintCbUnitOfMeasurement = "Unidad de medida",
                itemsCbUnitOfMeasurement = new string[] { "Unidad", "Centímetro", "Milimetro", "Metro", "Litro", "Kilo", "Gramo", "Hora", "Minuto" },
                hintCbTaxClassification = "Clasificación tributaria",
                itensCbTaxClassification = new string[] { "Excluido", "Exento", "Gravado" },
                hintTbStore = "Bodega",

            
                toolTipCbSupplier = "Selecionar Proveedor",
                hintTbNameSupplier = "Nombre",
                hintTbPurchaseValue = "Vr. de Compra",
                hintCbVAT = "INP",
                hintTbPurchaseValueWithVAT = "Vr. de Compra con IVA",
                hintTbtbWinPercentage = "% de ganancia",
                hintTbSaleValue = "Vr. de Venta",
                contentCbInvoiceWithoutExistence = "Facturar sin existencia",
                contentChVatIncludedInTheSaleValue = "IVA incluido al Vr. de venta",
                hintTbCantMin = "Cantidad Mínima"
            };

            // Email
            data[8] = new
            {
                hintTbTo = "Para",
                hintTbSubject = "Asunto",
                hintTbContent = "Mensaje",
                toolTipBtClose = "Cerrar",
                toolTipBtSend = "Enviar",
                toolTipBtAttached = "Adjuntar",
                contentBtDelete = "Eliminar"
            };

            // Login
            data[9] = new
            {
                contentTbWelcome = "Bienvenido de Nuevo!",
                contentTbInfo = "Inicie sesión en una cuenta existente",
                hintTbEmail = "Correo Electrónico",
                hintTbPassword = "Contraseña",
                contentBtnLogin = "INICIAR SESIÓN",
                contentBtnSignup = "Crear una Cuenta",
                contentBtnHelp = "Ayuda",
                contentBtnExit = "Cerrar",
                toolTipBtnHelp = "Teniendo problemas para iniciar sesión?",
                toolTipBtnExit = "Salir de la Aplicación"
            };


            // PageUnitOfMeasurement
            data[10] = new
            {
                toolTipBtnNew = "Nueva unidad de medida",
                toolTipBtnDelete = "Eliminar unidad de medida",
                toolTipBtnEdit = "Editar unidad de medida",
                toolTipBtnExport = "Exportar datos"
            };

            MiliFileEngine.src.Resources.MiliResources.Externs.SetString(MiliFileEngine.src.Resources.ResHash.GetHash("es-CO"), Newtonsoft.Json.JsonConvert.SerializeObject(data));
        }
    }
}
