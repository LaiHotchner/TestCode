using CodeSqlGenerate.Generate.JavaCode.Curd;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate.JavaCode
{
    public static class Backend_DeviceManagement
    {
        #region 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string deviceFolder = "devices\\";
        public static readonly string devicePackage = ".devices";
        #endregion

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + devicePackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + devicePackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + devicePackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + devicePackage;
        internal static readonly string DeviceServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + devicePackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(deviceFolder,
                out string entityFolderPath,
                out string daoFolderPath,
                out string mapperFolderPath,
                out string controllerFodlerPath,
                out string serviceImplFolderPaht,
                out string serviceInterfaceFolderPath);

            foreach (var table in tableList)
            {
                var daoContent = Dao.GetContent(table);
                var mapperContent = Mapper.GetContent(table);
                var controllerContent = Controller.GetContent(table);
                var serviceImplContent = ServiceImpl.GetContent(table);
                var serviceInterfaceContent = ServiceInterface.GetContent(table);

                var daoFilePath = daoFolderPath + GetDaoClassName(table) + ".java";
                var mapperFilePath = mapperFolderPath + GetMapperFileName(table) + ".xml";
                var controllerFilePath = controllerFodlerPath + GetControllerClassName(table) + ".java";
                var serviceImplFilePath = serviceImplFolderPaht + GetServiceImplClassName(table) + ".java";
                var serviceInterfaceFilePath = serviceInterfaceFolderPath + GetServiceInterfaceClassName(table) + ".java";

                File.WriteAllText(daoFilePath, daoContent, new UTF8Encoding(false));
                File.WriteAllText(mapperFilePath, mapperContent, new UTF8Encoding(false));
                File.WriteAllText(controllerFilePath, controllerContent, new UTF8Encoding(false));
                File.WriteAllText(serviceImplFilePath, serviceImplContent, new UTF8Encoding(false));
                File.WriteAllText(serviceInterfaceFilePath, serviceInterfaceContent, new UTF8Encoding(false));
            }

            Entity.GenerateAllEntity(tableList, entityFolderPath);
        }


        internal static string GetEntityName(HotchnerTable table)
        {
            return table.PascalMethodName;
        }
        internal static string GetDaoClassName(HotchnerTable table)
        {
            return table.PascalMethodName + "Dao";
        }
        internal static string GetMapperFileName(HotchnerTable table)
        {
            return table.PascalMethodName + "Mapper";
        }
        internal static string GetControllerClassName(HotchnerTable table)
        {
            return table.PascalMethodName + "Controller";
        }
        internal static string GetServiceImplClassName(HotchnerTable table)
        {
            return table.PascalMethodName + "ServiceImpl";
        }
        internal static string GetServiceInterfaceClassName(HotchnerTable table)
        {
            return table.PascalMethodName + "Service";
        }

        #region Restful_Api_Name
        internal static string GetCreateMethodName(HotchnerTable table)
        {
            return $"create{table.PascalMethodName}";
        }
        internal static string GetImportMethodName(HotchnerTable table)
        {
            return $"import{table.PascalMethodName}";
        }
        internal static string GetImportConfirmMethodName(HotchnerTable table)
        {
            return $"import{table.PascalMethodName}Confirm";
        }
        internal static string GetDeleteByIdMethodName(HotchnerTable table)
        {
            return $"delete{table.PascalMethodName}ById";
        }
        internal static string GetUpdateMethodName(HotchnerTable table)
        {
            return $"update{table.PascalMethodName}";
        }
        internal static string GetByIdMethodName(HotchnerTable table)
        {
            return $"get{table.PascalMethodName}ById";
        }
        internal static string GetAllMethodName(HotchnerTable table)
        {
            return $"getAll{table.PascalMethodName}";
        }
        #endregion
    }
}
