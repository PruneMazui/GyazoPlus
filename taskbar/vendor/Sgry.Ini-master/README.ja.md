概要
====
Sgry.IniはC#で実装されたINIファイル形式のパーサです。制約の緩いzlib/libpngライ
センスの下、単一ソースファイルで実装しています。

サポートするINI形式
===================
INI形式自体については以下Wikipediaの説明をご参照ください。

- [INIファイル (日本語)](http://ja.wikipedia.org/wiki/INI%E3%83%95%E3%82%A1%E3%82%A4%E3%83%AB)
- [INI file (English)](http://en.wikipedia.org/wiki/INI_file)

INI形式には様々な亜種が存在しますが、このクラスが対象としているのは以下の形式で
す：

- 空行は無視
- コメント行
  - 半角セミコロン(;)で始まる行をコメントとみなす
  - 行頭の空白は無視
  - プロパティの行やセクション開始行の後ろにコメントを付けることはできない
- セクション開始行
  - '[' で始まり ']' で終わる行をセクション開始行とみなす
  - 行頭と行末の空白は無視
  - 最初の '[' と最後の ']' の間にある文字列をセクション名と判断
- プロパティ定義行
  - 上記いずれでもない行で、等号 '=' がある行をプロパティ定義の行とみなす
  - 行頭から最初の等号 '=' までをプロパティ名とみなす
  - 最初の等号 '=' の直後から行末までを、プロパティの値とみなす
  - プロパティ名の先頭と末尾に空白文字がある場合、それらは無視する
- その他
  - セクション名・プロパティ名の大文字小文字の区別を行うかどうかを指定可能
  - グローバルプロパティに対応
    - API上、セクション名に空文字列 "" を指定することでアクセス可能
  - エスケープ記法には対応していません
  - プロパティ定義の区切り文字は等号 '=' のみに対応


機能
====
ファイルや文字列からロード・セーブする
--------------------------------------
INIドキュメントの内容を解析・ロードするにはIniDocumentのLoadメソッドを使います。
Loadメソッドは System.IO.TextReader からデータを読み出すため、ファイルから読み
出す場合は System.IO.StreamReader を、メモリ上の文字列(string変数)から読み出す
場合は System.IO.StringReader を指定してください。次に例を示します。

    var ini = new IniDocument();
    
    // Load INI data in a file
    using( var file = new StreamReader("data.ini", Encoding.UTF8) )
        ini.Load( file );
    
    // Load INI data on the memory (String object)
    var str = "...(INI data here)...";
    ini.Load( new StringReader(str) );

INIドキュメントの内容をシリアライズするには、IniDocumentのSaveメソッドを使いま
す。SaveメソッドはLoadメソッドと同様に System.IO.TextWriter にデータを出力しま
す。次に例を示します。

    var ini = new IniDocument();
    
    // いくつか値を設定する
    ini.Set("", "foo", "bar");
    ini.Set("[section]", "Foo", "Bar");
    
    // ファイルに保存
    using( var file = new StreamWriter("data.ini", false, Encoding.UTF8) )
    {
        file.NewLine = "\r\n";
        ini.Save( file );
    }
    
    // メモリ上のStringBuilderに保存
    var buf = new StringBuilder();
    var writer = new StringWriter( buf );
    writer.NewLine = "\r\n";
    ini.Save( new StringWriter(buf) );

なおIniDocumentのSaveメソッドは空のセクション、つまりプロパティが一つも無いセク
ションを出力しません。もしそれが必要な場合、IniDocumentのSectionsプロパティを
使って自前の出力処理を実装してください。


値の取り出し
------------
INIデータの読み出しはIniDocumentのGetメソッドで行います。Getメソッドでは
string型に限らず、他の基本的なデータも扱えます。次に例を示します。

    /* ファイル data.ini の内容:
    [Profile]
    Name=Suguru
    IsGeek=True
    Age=31
    */
    
    // ファイルの内容をロード
    var ini = new IniDocument();
    using( var file = new StreamReader("data.ini", Encoding.UTF8) )
        ini.Load( file );
    
    // データを取り出す
    ini.Get( "Profile", "Name", null ); // "Suguru" が返る
    ini.Get( "Profile", "IsGeek", false ); // true が返る
    ini.GetInt( "Profile", "Age", 0, Int32.MaxValue, 0 ); // 31 が返る
    ini.Get( "Profile", "Address", "unknown" ); // "unknown" が返る

Getメソッドは、Iniファイルの主な用途が設定ファイルであることを考えて第3引数で
「デフォルト値」を指定できるようになっています。また値が見つかったどうかを返す
TryGetメソッドも使用できます。

数値の取得用には、専用のメソッドGetIntがあります。もちろんGetメソッドでも数値は
取得できますが、GetIntは範囲チェックも追加で行います。第3引数と第4引数に適切な
「許容する最小値と最大値」を指定すれば、読み出し後のチェックを省略できます。


値の変更や追加
--------------
内容を変更する場合はIniDocumentのSetメソッドを使います。指定したプロパティがす
でに存在すればプロパティの値が上書きされ、存在しなければプロパティが新しく登録
されます。なおSetメソッドは内部で値として指定されたオブジェクトのToStringメ
ソッドを呼び出すため、どのような型のオブジェクトでも指定可能です。


プロパティ・セクションの削除
----------------------------
IniDocumentのRemoveメソッドを使うとセクションやプロパティを削除することができま
す。


利用にあたって
==============
このライブラリは [zlib/libpngライセンス](LICENSE.md) の下で自由に使用することが
可能です。zlib/libpngライセンスでは、バイナリを再頒布する場合よりソースを製品に
リンクする方が制限が緩くなります。ソースだけを同梱して使いたい場合、
Source/Sgry.Ini/Ini.cs だけを取り出して組み込んでください。
