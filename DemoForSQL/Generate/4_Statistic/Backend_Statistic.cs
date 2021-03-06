﻿using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._4_Statistic.Backend;
using System.Collections.Generic;

namespace CodeSqlGenerate.Generate._4_Statistic
{
    public class Backend_Statistic
    {
        public static readonly string FieldPrefix = "statistic";

        // 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string StatisticFolder = "statistic\\";
        public static readonly string StatisticPackage = ".statistic";

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + StatisticPackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + StatisticPackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + StatisticPackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + StatisticPackage;
        internal static readonly string ServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + StatisticPackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(StatisticFolder,
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
