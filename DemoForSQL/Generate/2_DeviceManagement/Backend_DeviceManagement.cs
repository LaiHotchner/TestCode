using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._2_DeviceManagement.Backend;
using System.Collections.Generic;

namespace CodeSqlGenerate.Generate._2_DeviceManagement
{
    public static class Backend_DeviceManagement
    {
        public static readonly string FieldPrefix = "devices";

        // 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string DeviceFolder = "devices\\";
        public static readonly string DevicePackage = ".devices";

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + DevicePackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + DevicePackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + DevicePackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + DevicePackage;
        internal static readonly string DeviceServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + DevicePackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(DeviceFolder,
                out var entityFolderPath,
                out var daoFolderPath,
                out var mapperFolderPath,
                out var controllerFolderPath,
                out var serviceImplFolderPath,
                out var serviceInterfaceFolderPath);

            Entity.Generate(entityFolderPath, tableList);
            Dao.Generate(daoFolderPath, tableList);
            Mapper.Generate(mapperFolderPath, tableList);
            Controller.Generate(controllerFolderPath, tableList);
            ServiceImpl.Generate(serviceImplFolderPath, tableList);
            ServiceInterface.Generate(serviceInterfaceFolderPath, tableList);
        }

        internal static string GetEntityName(HotchnerTable table) { return table.PascalMethodName; }
        internal static string GetDaoClassName(HotchnerTable table) { return table.PascalMethodName + "Dao"; }
        internal static string GetMapperFileName(HotchnerTable table) { return table.PascalMethodName + "Mapper"; }
        internal static string GetControllerClassName(HotchnerTable table) { return table.PascalMethodName + "Controller"; }
        internal static string GetServiceImplClassName(HotchnerTable table) { return table.PascalMethodName + "ServiceImpl"; }
        internal static string GetServiceInterfaceClassName(HotchnerTable table) { return table.PascalMethodName + "Service"; }


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
