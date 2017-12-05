
using System.Collections.Generic;
/**
* 黑板类 用来保存行为树中使用的数据
* 
* */
using UnityEngine;
namespace TFrame
{
    public class BPBlackBoard: MonoBehaviour
    {
        private Dictionary<string, object> _datas = new Dictionary<string, object>();
        
        public T GetDataByName<T>(string dataName)
        {
            if (_datas.ContainsKey(dataName))
                return (T)_datas[dataName];
            else
                return default(T);
        } 
        public void SetData<T>(string dataName, T data)
        {
            if (_datas.ContainsKey(dataName))
                _datas[dataName] = data;
            else
                _datas.Add(dataName, data);
        }
    }
}