using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._5_Anchors.Backend;
using System.Collections.Generic;

namespace CodeSqlGenerate.Generate._5_Anchors
{
    public static class Backend_Anchors
    {
        public static readonly string FieldPrefix = "anchors";

        // 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string ArchorsFolder = "anchors\\";
        public static readonly string ArchorsPackage = ".anchors";

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + ArchorsPackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + ArchorsPackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + ArchorsPackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + ArchorsPackage;
        internal static readonly string ServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + ArchorsPackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(ArchorsFolder,
                out var entityFolderPath,
                out var daoFolderPath,
                out var mapperFolderPath,
                out var controllerFolderPath,
                out var serviceImplFolderPath,
                out var serviceInterfaceFolderPath);

            Entity.Generate(entityFolderPath);
            Dao.Generate(daoFolderPath, tableList);
            Mapper.Generate(mapperFolderPath, tableList);
            Controller.Generate(controllerFolderPath, tableList);
            ServiceImpl.Generate(serviceImplFolderPath, tableList);
            ServiceInterface.Generate(serviceInterfaceFolderPath, tableList);
        }

        internal static readonly string NamePrefix = "Anchors";
        internal static string DaoName = $"{NamePrefix}Dao";
        internal static string MapperName = $"{NamePrefix}Mapper";
        internal static string ControllerName = $"{NamePrefix}Controller";
        internal static string ServicesName = $"{NamePrefix}Service";
        internal static string ServicesImplName = $"{NamePrefix}ServiceImpl";


        // Restful_Api_Name
        // In controller, use this method
        public static string GetAnchorsByDeviceType_MethodName = $"get{NamePrefix}ByDeviceType";
        // In Dao, use this to query postion
        public static string GetPositions_EachTable(HotchnerTable table)
        {
            return $"get{table.PascalMethodName}Positions";
        }
    }
}
