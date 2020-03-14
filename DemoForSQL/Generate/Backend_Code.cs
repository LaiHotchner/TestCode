using CodeSqlGenerate.Utility;

namespace CodeSqlGenerate.Generate
{
    public static class Backend_Code
    {
        public static readonly string EntityPath = "entity\\";
        public static readonly string DaoPath = "dao\\";
        public static readonly string MapperPath = "mappers\\";
        public static readonly string ControllerPath = "controller\\";
        public static readonly string ServiceImplPath = "service\\impl\\";
        public static readonly string ServiceInterfacePath = "service\\";

        public static readonly string BaseEntityPackagePrefix = "com.infinite.icts.entity";
        public static readonly string BaseDaoPackagePrefix = "com.infinite.icts.dao";
        public static readonly string BaseControllerPackagePrefix = "com.infinite.icts.controller";
        public static readonly string BaseServiceImplPackagePrefix = "com.infinite.icts.service.impl";
        public static readonly string BaseServiceInterfacePackagePrefix = "com.infinite.icts.service";

        public static readonly string JavaCodeOutputPath = @"C:\0_Infinite\ICTS\2_Generated\JavaCode\";

        public static void PreProcessFolder(string folderPrefix,
            out string entityFolderPath,
            out string daoFolderPath,
            out string mapperFolderPath,
            out string controllerFodlerPath,
            out string serviceImplFolderPaht,
            out string serviceInterfaceFolderPath)
        {
            entityFolderPath = JavaCodeOutputPath + EntityPath + folderPrefix;
            daoFolderPath = JavaCodeOutputPath + DaoPath + folderPrefix;
            mapperFolderPath = JavaCodeOutputPath + MapperPath + folderPrefix;
            controllerFodlerPath = JavaCodeOutputPath + ControllerPath + folderPrefix;
            serviceImplFolderPaht = JavaCodeOutputPath + ServiceImplPath + folderPrefix;
            serviceInterfaceFolderPath = JavaCodeOutputPath + ServiceInterfacePath + folderPrefix;

            if (Program.IsOutputToProject)
            {
                entityFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + EntityPath + folderPrefix;
                daoFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + DaoPath + folderPrefix;
                mapperFolderPath = @"C:\0_Workspace\icts\src\main\resources\" + MapperPath + folderPrefix;
                controllerFodlerPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ControllerPath + folderPrefix;
                serviceImplFolderPaht = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ServiceImplPath + folderPrefix;
                serviceInterfaceFolderPath = @"C:\0_Workspace\icts\src\main\java\com\infinite\icts\" + ServiceInterfacePath + folderPrefix;
            }

            CommonMethod.CreateDirectoryIfNotExist(entityFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(daoFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(mapperFolderPath);
            CommonMethod.CreateDirectoryIfNotExist(controllerFodlerPath);
            CommonMethod.CreateDirectoryIfNotExist(serviceImplFolderPaht);
            CommonMethod.CreateDirectoryIfNotExist(serviceInterfaceFolderPath);

            if (!string.IsNullOrEmpty(folderPrefix))
            {
                CommonMethod.ClearFolderIfExistFiles(entityFolderPath);
                CommonMethod.ClearFolderIfExistFiles(daoFolderPath);
                CommonMethod.ClearFolderIfExistFiles(mapperFolderPath);
                CommonMethod.ClearFolderIfExistFiles(controllerFodlerPath);
                CommonMethod.ClearFolderIfExistFiles(serviceImplFolderPaht);
                CommonMethod.ClearFolderIfExistFiles(serviceInterfaceFolderPath);
            }
        }

        public static string GetPagingParameterEntity()
        {
            return "com.infinite.common.entity.PagingParameter";
        }
    }
}
