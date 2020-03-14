using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._4_Statistic.Backend;
using System.Collections.Generic;

namespace CodeSqlGenerate.Generate._4_Statistic
{
    public class Backend_Statistic
    {
        public static readonly string FieldPrefix = "statistic";

        #region 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string statisticFolder = "statistic\\";
        public static readonly string statisticPackage = ".statistic";
        #endregion

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + statisticPackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + statisticPackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + statisticPackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + statisticPackage;
        internal static readonly string ServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + statisticPackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(statisticFolder,
            out string entityFolderPath,
            out string daoFolderPath,
            out string mapperFolderPath,
            out string controllerFodlerPath,
            out string serviceImplFolderPaht,
            out string serviceInterfaceFolderPath);

            Entity.GenerateAllEntity(entityFolderPath);
            Controller.Generate(controllerFodlerPath, tableList);
            //ServiceImpl.Generate(serviceImplFolderPaht, tableList);
            ServiceInterface.Generate(serviceInterfaceFolderPath, tableList);
        }

        internal static readonly string NamePrefix = "Statistic";
        internal static string DaoName = $"{NamePrefix}Dao";
        internal static string MapperName = $"{NamePrefix}Mapper";
        internal static string ControllerName = $"{NamePrefix}Controller";
        internal static string ServicesName = $"{NamePrefix}Service";
        internal static string ServicesImplName = $"{NamePrefix}ServiceImpl";

        // Restful_Api_Name
        public static string GetAllStatisticResult_MethodName = $"getAll{NamePrefix}Result";
    }
}
