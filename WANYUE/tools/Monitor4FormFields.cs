using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using Kingdee.BOS;
using Kingdee.BOS.Util;
using Kingdee.BOS.Core;
using Kingdee.BOS.Core.DynamicForm.PlugIn;
using Kingdee.BOS.Core.DynamicForm.PlugIn.Args;
using Kingdee.BOS.Core.Bill;
using Kingdee.BOS.Core.Bill.PlugIn;
using Kingdee.BOS.Core.Bill.PlugIn.Args;
using Kingdee.BOS.Core.Metadata.FieldElement;
using Kingdee.BOS.Orm.DataEntity;

namespace Kingdee.K3.WANYUE.PlugIn.service.tools
{
        /// <summary>
        /// 监控调用Web API接口保存单据时，给字段赋值过程
        /// </summary>
        /// <remarks>
        /// 背景说明：
        /// 调用Web API或者WebService接口，保存单据；
        /// 经常发生参数中已经设置了字段值，但是保存结果，却提示字段必录；
        /// 发生此现象时，非常难以排查问题；
        /// 
        /// 解决方案：
        /// 发生此现象时，可以把本插件，挂在单据表单插件上；
        /// 本插件监控到单据被Web API接口，设置单据字段值时，
        /// 会即时把字段的历史数值、新数据输出到K/3 Cloud日志文件中，
        /// 通过日志文件，就可以了解字段赋值的顺序、所填写的值，
        /// 确认出字段为何没有填写上：
        /// 1. 字段未接收到值：字段Key错误，或字段值不合法
        /// 2. 被其他字段覆盖了：字段录入顺序需要调整
        /// </remarks>
        [Description("Web API接口赋值监控")]
        public class Monitor4FormFields : AbstractBillPlugIn
        {
            /// <summary>
            /// 当前进程，是否为WebService、Web API方式调用；
            /// 只有WebService调用，才输出日志
            /// </summary>
            bool _isWebService = false;
            /// <summary>
            /// 界面初始化事件：判断当前进程是否为WebService调用
            /// </summary>
            /// <param name="e"></param>
            public override void OnInitialize(InitializeEventArgs e)
            {
                if (this.Context.ServiceType == WebType.WebService)
                {
                    this._isWebService = true;
                }
            }
            /// <summary>
            /// 单据数据包创建、加载完毕：在日志中，输出一个开始语句，以便阅读
            /// </summary>
            /// <param name="e"></param>
            public override void AfterCreateNewData(EventArgs e)
            {
                if (this._isWebService == false)
                {
                    return;
                }
                string message = string.Format("现在开始创建单据{0}的新数据包",
                    this.View.BillBusinessInfo.GetForm().Name.ToString());
                Kingdee.BOS.Log.Logger.Info("WebService", message);
            }
            /// <summary>
            /// 值改变事件：监控字段值修改顺序及值
            /// </summary>
            /// <param name="e"></param>
            public override void DataChanged(DataChangedEventArgs e)
            {
                if (this._isWebService)
                {
                    string message = this.BuildFldChangedLog(e.Field, e.NewValue, e.OldValue);
                    Kingdee.BOS.Log.Logger.Info("WebService", message);
                }
            }
            /// <summary>
            /// 保存前调用此事件：可以通过此事件，观察单据数据包中的字段值
            /// </summary>
            /// <param name="e"></param>
            public override void BeforeSave(BeforeSaveEventArgs e)
            {
                base.BeforeSave(e);
            }
            /// <summary>
            /// 构建字段的修改日志
            /// </summary>
            /// <param name="field"></param>
            /// <param name="newValue"></param>
            /// <param name="oldValue"></param>
            /// <returns></returns>
            private string BuildFldChangedLog(Field field, object newValue, object oldValue)
            {
                string newValueString = Convert.ToString(newValue);
                string oldValueString = Convert.ToString(oldValue);
                string logMessage = string.Format("给字段【{0}({1})】赋值为 \"{2}\"；旧值为\"{3}\"{4}",
                    field.Name.ToString(),
                    field.Key,
                    string.IsNullOrWhiteSpace(newValueString) ? "(空值)" : newValueString,
                    string.IsNullOrWhiteSpace(oldValueString) ? "(空值)" : oldValueString,
                    string.IsNullOrWhiteSpace(newValueString) ? "【有异常，清空了字段值！】" : string.Empty);
                return logMessage;
            }
        }
    }

