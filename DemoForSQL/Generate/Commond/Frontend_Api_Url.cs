using CodeSqlGenerate.Data;
using CodeSqlGenerate.Generate._2_DeviceManagement;
using CodeSqlGenerate.Generate._3_Retrieval;
using CodeSqlGenerate.Generate._5_Anchors;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CodeSqlGenerate.Generate.Commond
{
    public static class Frontend_Api_Url
    {
        public static void GenerateCode(List<HotchnerTable> tableList)
        {
            var apiFolder = Frontend_Code.FrontendOutputPath;
            if (Program.IsOutputToProject)
            {
                apiFolder = @"C:\0_Workspace\ict\src\app\common\";
            }

            StringBuilder stringBuilder = new StringBuilder();
            #region Base URL
            stringBuilder.AppendLine("import {HTTP_REST_BASE_URL} from './ip-config';");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("export class API {");
            stringBuilder.AppendLine("  // 3D Base Url");
            stringBuilder.AppendLine("  public static USBD_3D_BASE_URL = 'http://192.168.3.213:8090/iserver/services/';");
            stringBuilder.AppendLine("  //  高程");
            stringBuilder.AppendLine("  public static UAVS_3D_WORLD_CHINA_ZJDEM = API.USBD_3D_BASE_URL + '3D-worldscene/rest/realspace/datas/worldchinazjdem';");
            stringBuilder.AppendLine("  // 地图影像");
            stringBuilder.AppendLine("  public static UAVS_3D_YING_XIANG = API.USBD_3D_BASE_URL + 'map-worldscene/rest/maps/worldyingxiang0018web%40worldyingxiang0018web';");
            stringBuilder.AppendLine("  // 三维场景");
            stringBuilder.AppendLine("  public static UAVS_3D_SCENE = API.USBD_3D_BASE_URL + '3D-worldscene/rest/realspace';");
            stringBuilder.AppendLine("  // 所有点坐标");
            stringBuilder.AppendLine($"  public static {Frontend_Anchors.Get_AnchorsByType_Url} = HTTP_REST_BASE_URL + '/device/{Backend_Anchors.GetAnchorsByDeviceType_MethodName}';");
            stringBuilder.AppendLine("  // 检索设备信息");
            stringBuilder.AppendLine($"  public static {Frontend_Retrieval.Get_RetrievalAll_Url} = HTTP_REST_BASE_URL + '/device/{Backend_Retrieval.GetRetrievalAllMethodName}';");
            stringBuilder.AppendLine("  // 检索设备详细信息");
            stringBuilder.AppendLine($"  public static {Frontend_Retrieval.Get_RetrievalDetail_Url} = HTTP_REST_BASE_URL + '/device/{Backend_Retrieval.GetRetrievalDetailMethodName}';");
            stringBuilder.AppendLine("  // 获取设备统计信息");
            stringBuilder.AppendLine("  public static STATISTICAL_DEVICE = HTTP_REST_BASE_URL + '/device/getStatisticalResult';");
            stringBuilder.AppendLine("");
            #endregion
            #region CURD URL
            foreach (var table in tableList)
            {
                stringBuilder.AppendLine("  /**");
                stringBuilder.AppendLine("   * " + table.TableDescription + "API");
                stringBuilder.AppendLine("   */");
                stringBuilder.AppendLine("    // " + table.TableDescription + "导入");
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_Upload_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetImportMethodName(table)}';");
                stringBuilder.AppendLine("  // " + table.TableDescription + "导入确认");
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_Upload_Confirm_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetImportConfirmMethodName(table)}';");
                stringBuilder.AppendLine("  // 增加" + table.TableDescription);
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_Add_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetCreateMethodName(table)}';");
                stringBuilder.AppendLine("  // 修改" + table.TableDescription);
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_Update_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetUpdateMethodName(table)}';");
                stringBuilder.AppendLine("  // 修改" + table.TableDescription);
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_Delete_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetDeleteByIdMethodName(table)}';");
                stringBuilder.AppendLine("  // 查询全部" + table.TableDescription);
                stringBuilder.AppendLine($"  public static {Frontend_DeviceManagement.Get_FIND_ALL_Url(table)} = HTTP_REST_BASE_URL + '/device/{Backend_DeviceManagement.GetAllMethodName(table)}';");
                stringBuilder.AppendLine("");
            }
            #endregion
            stringBuilder.AppendLine("}");

            var apiTsFilePath = apiFolder + "api.ts";
            var apiTsContent = stringBuilder.ToString();
            File.WriteAllText(apiTsFilePath, apiTsContent, new UTF8Encoding(false));
        }
    }
}
