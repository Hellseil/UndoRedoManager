using System.Collections;
using System.Collections.Generic;

namespace UndoRedoManager
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	/// <typeparam name="TMemento"></typeparam>
	public class SnapshotManager<TKey,TMemento>:IEnumerable<TMemento>
    {

		public TMemento this[TKey key]
		{
			get
			{
				return this[key];
			}
		}

        /// <summary>
        /// 発信者
        /// </summary>
        IMementoOriginator<TMemento > _originator;
        public  SnapshotManager(IMementoOriginator<TMemento> originator ) 
        {
            this._originator = originator;
			this._memenotDicionary = new Dictionary<TKey, TMemento>();
        }
        protected Dictionary<TKey, TMemento> _memenotDicionary;


		public virtual void SaveSnapShort(TKey key, bool isOverwrite=false)
        {
			if (!this._memenotDicionary.ContainsKey(key) && !isOverwrite)
			{
				this._memenotDicionary.Add(key, this._originator.CreateMemento());
			}
			else
			{
				this._memenotDicionary[key] = this._originator.CreateMemento();
			}
        }
        public virtual bool LoadSnapShort(TKey key)
        {
            var ret = false;
            if(this._memenotDicionary.ContainsKey(key))
            {
				this._originator.SetMemento(this._memenotDicionary[key]);
                ret = true;
            }

            return ret;
        }
       public virtual bool RemoveSnapShort(TKey key) 
        {
            var ret = false;
            if (this._memenotDicionary.ContainsKey(key))
            {
				this._memenotDicionary.Remove (key);
                ret = true;
            }
            return ret;
        }
        public virtual void ResetSnapShort()
        {
			this._memenotDicionary.Clear();
        }

		public IEnumerator<TMemento> GetEnumerator()
		{
			return this._memenotDicionary.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._memenotDicionary.Values.GetEnumerator();
		}

	}
}
