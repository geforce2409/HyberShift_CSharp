using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyberShift_CSharp.Model.List
{
    public abstract class BaseList<T>
    {
        private ObservableCollection<T> list;

        // getter and setter
        public ObservableCollection<T> List
        {
            get { return list; }
            set
            {
                list.Clear();
                list = value;
            }
        }

        // constructor
        public BaseList()
        {
            list = new ObservableCollection<T>();
        }

        // method
        /// <summary>
        /// Lấy ra một collections thõa mãn giá trị cho trước thông qua một thuộc tính
        /// Vd: list chứa thông tin của những UserOnline, 
        /// UserOnline gồm các thuộc thuộc tính Name, Email 
        /// => GetCollectionOfField("Name", "Tran Minh Quan") trả về một collection chứa các UserOnline có Name là "Tran Minh Quan" của list
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public ObservableCollection<T> GetCollectionByValue(string fieldName, dynamic value)
        {
            ObservableCollection<T> result = new ObservableCollection<T>();
            foreach (T obj in list)
            {
                if (BaseList<T>.GetPropertyValue(obj, fieldName) == value)
                    result.Add(obj);
            }
            return result;
        }

        /// <summary>
        ///  Lấy ra đối tượng đầu tiên thõa mãn giá trị cho trước thông qua một thuộc tính
        /// Vd: list chứa thông tin của những UserOnline, 
        /// UserOnline gồm các thuộc thuộc tính Name, Email 
        /// => GetCollectionOfField("Name", "Tran Minh Quan") trả về một đối tượng UserOnline có Name là "Tran Minh Quan" của list
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public T GetFirstObjectByValue(string fieldName, dynamic value)
        {
            foreach (T obj in list)
            {
                if (BaseList<T>.GetPropertyValue(obj, fieldName) == value)
                    return obj;
            }
            return default(T);
        }

        /// <summary>
        /// Lấy ra một collections chứa các giá trị của một thuộc tính
        /// Vd: list chứa thông tin của những UserOnline, 
        /// UserOnline gồm các thuộc thuộc tính Name, Email 
        /// => GetCollectionOfField("Name") trả về một collection chứa các Name của list
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public ObservableCollection<dynamic> GetCollectionOfField(string fieldName)
        {
            ObservableCollection<dynamic> result = new ObservableCollection<dynamic>();
            foreach (T obj in list)
            {
                result.Add(BaseList<T>.GetPropertyValue(obj, fieldName));
            }
            return result;
        }

        /// <summary>
        /// Lấy ra một collections chứa các giá trị của một thuộc tính nếu thõa mãn giá trị value
        /// Vd: list chứa thông tin của những UserOnline, 
        /// UserOnline gồm các thuộc thuộc tính Name, Email 
        /// => GetCollectionOfFieldByValue("Name", "Tran Minh Quan") trả về một collection chứa các Name của list có giá trị là "Tran Minh Quan"
        /// </summary>
        /// <param name="fieldNameToGet">tên thuộc tính cần lấy</param>
        /// <param name="fieldNameToCompare">tên thuộc tính cần đối chiếu</param>
        /// <param name="value">giá trị thuộc tính cần đối chiếu</param>
        /// <returns></returns>
        public ObservableCollection<dynamic> GetCollectionOfFieldByValue(string fieldNameToGet, string fieldNameToCompare, dynamic value)
        {
            ObservableCollection<dynamic> result = new ObservableCollection<dynamic>();
            foreach (T obj in list)
            {
                if (BaseList<T>.GetPropertyValue(obj, fieldNameToCompare) == value)
                    result.Add(BaseList<T>.GetPropertyValue(obj, fieldNameToGet));
            }
            return result;
        }

        /// <summary>
        /// Lấy ra một đối tượng chứa các giá trị của một thuộc tính nếu thõa mãn giá trị value
        /// Vd: list chứa thông tin của những UserOnline, 
        /// UserOnline gồm các thuộc thuộc tính Name, Email 
        /// => GetCollectionOfFieldByValue("Name", "Tran Minh Quan") trả về một đối tượng chứa các Name của list có giá trị là "Tran Minh Quan"
        /// </summary>
        /// <param name="fieldNameToGet"></param>
        /// <param name="fieldNameToCompare"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public dynamic GetFirstObjectOfFieldByValue(string fieldNameToGet, string fieldNameToCompare, dynamic value)
        {
            foreach (T obj in list)
            {
                if (BaseList<T>.GetPropertyValue(obj, fieldNameToCompare) == value)
                    return obj;
            }
            return null;
        }

        public int GetIndexByValue(string fieldNameToCompare, dynamic value)
        {
            for(int i=0; i<list.Count; i++)
            {
                if (BaseList<T>.GetPropertyValue(list[i], fieldNameToCompare) == value)
                    return i;
            }
            return -1;
        }

        public dynamic GetFieldValueByIndex(string fieldNameToGet, int index)
        {
            return BaseList<T>.GetPropertyValue(list[index], fieldNameToGet);
        }

        /// <summary>
        /// Thêm phần tử vào list (KHÔNG kiểm tra trùng lắp)
        /// </summary>
        /// <param name="obj">object cần thêm</param>
        public void Add(T obj)
        {
            list.Add(obj);
        }

        /// <summary>
        /// Thêm phần tử vào list (có kiểm tra trùng lắp)
        /// </summary>
        /// <param name="obj">object cần thêm</param>
        /// <param name="propertyName">tên thuộc tính cần kiểm tra</param>
        public void AddWithCheck(T obj, string propertyName)
        {
            foreach (T element in list)
            {
                // if duplicated
                if (BaseList<T>.GetPropertyValue(element, propertyName).Equals(BaseList<T>.GetPropertyValue(obj, propertyName)))
                    return;
            }
            list.Add(obj);
        }

        /// <summary>
        /// Lấy giá trị của thuộc tính
        /// </summary>
        /// <param name="obj">object cần lấy thuộc tính</param>
        /// <param name="propertyName">tên thuộc tính kiểu string</param>
        /// <returns>Kiểu object chứa giá trị của thuộc tính đó</returns>
        public static dynamic GetPropertyValue(dynamic obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj);
        }
    }
}
