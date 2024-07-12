/******************************************************************************
 * ファイル	：IUndoRedoble.cs
 * 目的		：
 * 名前空間	:UndoRedoManager
 * 依存関係	：
 * 注意点	：
 * 備考		：
 * Netver	：4.8
 * 変更履歴
 *	2024/##/##	user		新規作成
*******************************************************************************/


//-----------------------------------------------
#region 使用する名前空間
#endregion

namespace UndoRedoManager
{
	public interface IUndoRedoble
	{
		/// <summary>
		/// 巻き戻し可能
		/// </summary> 
		bool UseUndo { get; }
		/// <summary>
		/// やり直し可能
		/// </summary>
		bool UseRedo {get; }
		/// <summary>
		/// 元に戻します。
		/// </summary>
		void Undo();
		/// <summary>
		/// やり直しをします。
		/// </summary>
		void Redo();
		/// <summary>
		/// 履歴をリセットします。
		/// </summary>
		void ResetUndoRedo();

	}
}
