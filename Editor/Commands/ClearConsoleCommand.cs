using System.Reflection;
using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// Console ウィンドウをクリアするコマンドを管理するクラス
	/// </summary>
	public sealed class ClearConsoleCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( ClearConsoleCommand );

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			const BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;

			var assembly   = Assembly.GetAssembly( typeof( EditorApplication ) );
			var type       = assembly.GetType( "UnityEditor.LogEntries" );
			var methodInfo = type.GetMethod( "Clear", bindingAttr );

			methodInfo.Invoke( null, null );

			return Success();
		}
	}
}