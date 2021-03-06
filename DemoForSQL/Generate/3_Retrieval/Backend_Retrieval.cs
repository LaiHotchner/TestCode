﻿using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._3_Retrieval.Backend;
using System.Collections.Generic;

namespace CodeSqlGenerate.Generate._3_Retrieval
{
    public static class Backend_Retrieval
    {
        public static readonly string FieldPrefix = "retrieval";

        // 这两个值要一致，保证package路径和文件夹路径一致
        public static readonly string RetrievalFolder = "retrieval\\";
        public static readonly string RetrievalPackage = ".retrieval";

        internal static readonly string EntityPackagePrefix = Backend_Code.BaseEntityPackagePrefix + RetrievalPackage;
        internal static readonly string DaoPackagePrefix = Backend_Code.BaseDaoPackagePrefix + RetrievalPackage;
        internal static readonly string ControllerPackagePrefix = Backend_Code.BaseControllerPackagePrefix + RetrievalPackage;
        internal static readonly string ServiceImplPackagePrefix = Backend_Code.BaseServiceImplPackagePrefix + RetrievalPackage;
        internal static readonly string ServiceInterfacePackagePrefix = Backend_Code.BaseServiceInterfacePackagePrefix + RetrievalPackage;

        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            Backend_Code.PreProcessFolder(RetrievalFolder,
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

        internal static readonly string NamePrefix = "Retrieval";
        internal static string DaoName = $"{NamePrefix}Dao";
        internal static string MapperName = $"{NamePrefix}Mapper";
        internal static string ControllerName = $"{NamePrefix}Controller";
        internal static string ServicesName = $"{NamePrefix}Service";
        internal static string ServicesImplName = $"{NamePrefix}ServiceImpl";

        #region Restful_Api_Name
        // 在Controller 层，暴露的是GetListMethodName
        public static string GetRetrievalAllMethodName = $"get{NamePrefix}List";
        // 在ServiceImpl层之后，需要调用不同设备的Dao_Retrieval去查询不同设备的信息，因此一个逻辑包含两个方法名
        public static string GetListMethodName_EachDao_GetRetrievalResult(HotchnerTable table)
        {
            return $"get{table.PascalMethodName}RetrievalResult";
        }


        // 在Controller层根据查询的设备类型，调用不同的 Device Service层，去查询不同的设备，因此不需要后续的Dao层
        public static string GetRetrievalDetailMethodName = $"get{NamePrefix}Detail";
        #endregion
    }
}
