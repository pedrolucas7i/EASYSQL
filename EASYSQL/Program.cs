
string init = """
    ___________   _____     ______________.___.  _________________   .____     
    \_   _____/  /  _  \   /   _____/\__  |   | /   _____/\_____  \  |    |    
     |    __)_  /  /_\  \  \_____  \  /   |   | \_____  \  /  / \  \ |    |    
     |        \/    |    \ /        \ \____   | /        \/   \_/.  \|    |___ 
    /_______  /\____|__  //_______  / / ______|/_______  /\_____\ \_/|_______ \
            \/         \/         \/  \/               \/        \__>        \/
    by Pedro Ribeiro Lucas (2024)
    v1.0

    """;


string registo = "";

string perguntaPadrao = "\nCHOOSE AN OPTION (DEFAULT 0)\n\n";

string op0 = "0 - GUIDED MODE\n";
string op1 = "1 - CREATE TABLE\n";
string op2 = "2 - CREATE FOREIGN KEY\n";
string op3 = "3 - INSERT DATA IN TABLE\n";

string e;

Console.WriteLine(init);
do
{
    switch (options())
    {
        case "0":
            guidedMode();
            break;

        case "1":
            file(createTable());
            break;

        case "2":
            break;

        case "3":
            file(insertInTable());
            break;

        default:
            guidedMode();
            break;
    }
    Console.WriteLine(perguntaPadrao + "0 - EXIT\n1 - NEW");
    e = Console.ReadLine();
} while ((e != "0") && (e != ""));

string options()
{
    Console.WriteLine(perguntaPadrao + op0 + op1 + op2 + op3 + "\n");
    return Console.ReadLine();
}

void guidedMode()
{
    string database;
    string nTabelas;
    int nTabelasInt;
    string tabelas = "";
    string required;
    string foreign = "";

    bool isRequired;

    do
    {
        Console.Write("Introduza o nome para a base de dados:\t");
        database = Console.ReadLine();
    }
    while (database == "" || isNumber(database));

    registo += "FEITO USANDO EASYSQL (v1.0)\n\n\nCREATE DATABASE " + database + ";\n\n" + "USE " + database + ";\n\n";
    
    do
    {
        Console.Write("Quantas tabelas pretende criar:\t");
        nTabelas = Console.ReadLine();
    }
    while (nTabelas == "" || !isNumber(nTabelas));
    nTabelasInt = int.Parse(nTabelas);

    for (int i = 0; i < nTabelasInt; i++)
    {
        tabelas += createTable();
    }

    registo += tabelas;

    foreignKey:
    do
    {
        Console.Write("ADICIONAR FOREIGN KEY (Y/N):\t");
        required = Console.ReadLine();

    } while (required != "Y" && required != "y" && required != "N" && required != "n");
    isRequired = (required == "Y" || required == "y") ? true : false;
   
    if (isRequired)
    {
        string foreignNameT;
        do
        {
            Console.Write("Nome da tabela no qual iremos inserir a FOREIGN KEY:\t");
            foreignNameT = Console.ReadLine();
        }
        while (foreignNameT == "" || isNumber(foreignNameT));
        foreign += "ALTER TABLE " + foreignNameT + "\n";

        string foreignNameC;
        do
        {
            Console.Write("Nome do campo a colocar como FOREIGN KEY:\t");
            foreignNameC = Console.ReadLine();
        }
        while (foreignNameC == "" || isNumber(foreignNameC));
        foreign += "ADD CONTRAINT fk_" + foreignNameC + "\nFOREIGN KEY (" + foreignNameC + ")\n";

        string tableReferences;
        do
        {
            Console.Write("Nome da tabela ao qual queremos ligar:\t");
            tableReferences = Console.ReadLine();
        }
        while (tableReferences == "" || isNumber(tableReferences));

        string campoReferences;
        do
        {
            Console.Write("Nome do campo da tabela " + tableReferences + " que queremos ligar:\t");
            campoReferences = Console.ReadLine();
        }
        while (campoReferences == "" || isNumber(campoReferences));

        foreign += "REFERENCES " + tableReferences + " (" + campoReferences + ");\n\n";
        registo += foreign;
    }

    do
    {
        Console.Write("ADICIONAR OUTRA FOREIGN KEY (Y/N):\t");
        required = Console.ReadLine();

    } while (required != "Y" && required != "y" && required != "N" && required != "n");
    isRequired = (required == "Y" || required == "y") ? true : false;
    if (isRequired)
    {
        goto foreignKey;
    }

    do
    {
        Console.Write("PREENCHER UMA TABELA (Y/N):\t");
        required = Console.ReadLine();

    } while (required != "Y" && required != "y" && required != "N" && required != "n");
    isRequired = (required == "Y" || required == "y") ? true : false;

    Console.Clear();
    Console.WriteLine(registo);

    if (isRequired)
    {
        string nTable;
        int numTables;
        string dataTable = "";
        do
        {
            Console.WriteLine("Quantas tabelas quer preencher:\t");
            nTable = Console.ReadLine();
        }
        while (nTable == "" || !isNumber(nTable));
        
        numTables = int.Parse(nTable);

        for (int i = 0; i < numTables; i++)
        {
            dataTable += insertInTable();
            Console.Clear();
            Console.WriteLine(registo + dataTable + "\n");
        }
        registo += dataTable + "\n\n";
    }

    file(registo);
}

string createTable()
{
    string table;
    string campos;
    string nomeCampo;
    string tCampo;
    string tamanho;
    string required;
    bool isRequired;
    string final = "";
    string totalCampos = "";

    do
    {
        Console.Write("QUAL O NOME DA TABELA A CRIAR:\t");
        table = Console.ReadLine();
    }
    while (table == "" || isNumber(table));


    do
    {
        Console.Write("QUANTOS CAMPOS TERÁ A TABELA:\t");
        campos = Console.ReadLine();
    }
    while (campos == "" || !isNumber(campos));

    int nCampos = int.Parse(campos);
    for (int i = 0; i < nCampos; i++)
    {

        do
        {
            Console.Write("NOME DO CAMPO:\t");
            nomeCampo = Console.ReadLine();
        } while (nomeCampo == "" || isNumber(nomeCampo));

        do
        {
            Console.WriteLine("\nTIPO DO CAMPO\n0 - INT\n1 - VARCHAR\n2 - FLOAT\n3 - DATE\n\n");
            tCampo = Console.ReadLine();
        } while (tCampo == "" || !isNumber(tCampo));

        do
        {
            tamanho = campo(tCampo);
        } while (tamanho == "ERRO");

        do
        {
            Console.Write("PRENCHIMENTO OBRIGATÓRIO (Y/N):\t");
            required = Console.ReadLine();

        } while (required != "Y" && required != "y" && required != "N" && required != "n");
        isRequired = (required == "Y" || required == "y") ? true : false;

        switch (tCampo)
        {
            case "0":
                tCampo = "INT";
                break;
            case "1":
                tCampo = "VARCHAR";
                break;
            case "2":
                tCampo = "FLOAT";
                break;
            case "3":
                tCampo = "DATE";
                break;
        }
        if (i == nCampos - 1)
        {
            totalCampos += nomeCampo + " " + tCampo + "(" + tamanho + ")" + (isRequired ? " NOT NULL\n" : "\n");
        } else
        {
            totalCampos += nomeCampo + " " + tCampo + "(" + tamanho + ")" + (isRequired ? " NOT NULL,\n" : ",\n");
        }
        final = "CREATE TABLE " + table + "(\n" + totalCampos + ");\n\n";
    }
    return final;
}

string campo(string op)
{
    string tamanho;
    tamanho = "";
    switch(op)
    {
        case "0":
            Console.Write("TAMANHO:\t");
            tamanho = Console.ReadLine();
            break;
        case "1":
            Console.Write("TAMANHO:\t");
            tamanho = Console.ReadLine();
            break;
        case "2":
            Console.Write("TAMANHO (ex. 6.2):\t");
            tamanho = Console.ReadLine();
            break;
        case "3":
            break;
        default:
            Console.Write("ERRO");
            tamanho = "ERRO";
            break;

    }
    return tamanho;
}

string insertInTable() {
    string nameTable = "";
    string campos = "";
    string campo = "";
    string camposTotal = "";
    string linhas = "";
    string linhaDados = "";
    string data = "";

    int ncampos;
    int nlinhas;

    do
    {
        Console.Write("Qual a tabela na qual quer inserir dados:\t");
        nameTable = Console.ReadLine();
    } while (nameTable == "" || isNumber(nameTable));

    do
    {
        Console.Write("Quantos campos tem a tabela:\t");
        campos = Console.ReadLine();
    } while (campos == "" || !isNumber(campos));
    ncampos = int.Parse(campos);

    string[] camposSeparated = new string[ncampos];
    string[] dado = new string[ncampos];

    for (int i = 0; i < ncampos; i++)
    {
        do
        {
            Console.Write("Nome do campo:\t");
            campo = Console.ReadLine();
        } while (campo == "" || isNumber(campo));
        Console.WriteLine("campo " + i + " ==> " + campo);
        if (!(i == ncampos - 1))
        {
            camposSeparated[i] = campo;
            camposTotal += campo + ", ";
        } else
        {
            camposSeparated[i] = campo;
            camposTotal += campo;
        }
    }

    do
    {
        Console.Write("Quantas linhas quer inserir na tabela:\t");
        linhas = Console.ReadLine();
    } while (linhas == "" || !isNumber(linhas));

    nlinhas = int.Parse(linhas);

    for (int i = 0; i < nlinhas; i++)
    {
        Console.WriteLine("Linha " + (i + 1) + "\n");
        for (int j = 0; j < ncampos; j++)
        {
            Console.Write("Valor de " + camposSeparated[j] + ":\t");
            dado[j] = Console.ReadLine();

            if (dado[j] == "")
            {
                dado[j] = "NULL";
            }
        }
        for (int  k = 0; k < ncampos; k++)
        {
            if (ncampos == 1)
            {
                if (dado[k] == "NULL")
                {
                    linhaDados += "("+ (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ")" : dado[k] + ")");
                }
                else
                {
                    linhaDados += "(" + (isNumber(dado[k]) == true ? int.Parse(dado[k]) + "')" : "'" + dado[k] + "')");
                }
            }
            if ((ncampos - k) == 1)
            {
                if (dado[k] == "NULL")
                {
                    linhaDados += (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ")" : dado[k] + ")");
                }
                else
                {
                    linhaDados += (isNumber(dado[k]) == true ? int.Parse(dado[k]) + "')" : "'" + dado[k] + "')");
                }
            } else if (k == 0)
            {
                if (dado[k] == "NULL")
                {
                    linhaDados += "(" + (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ", " : dado[k] + ", ");
                }
                else
                {
                    linhaDados += "(" + (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ", " : "'" + dado[k] + "', ");
                }
            } else
            {
                if (dado[k] == "NULL")
                {
                    linhaDados += (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ", " : dado[k] + ", ");
                }
                else
                {
                    linhaDados += (isNumber(dado[k]) == true ? int.Parse(dado[k]) + ", " : "'" + dado[k] + "', ");
                }
            }
        }

        if ((nlinhas - i) == 1)
        {
            linhaDados += ";\n\n";
        }
        else
        {
            linhaDados += ",\n";
        }
    }

    data = "INSERT INTO " + nameTable + "(" + camposTotal + ") VALUES\n" + linhaDados;
    return data;
}

void file(string output)
{
    string resposta;
    bool isRequired;
    do
    {
        Console.Write("Deseja guardar num ficheiro de texto (Y/N):\t");
        resposta = Console.ReadLine();

    } while (resposta != "Y" && resposta != "y" && resposta != "N" && resposta != "n");

    isRequired = (resposta == "Y" || resposta == "y") ? true : false;

    if (isRequired)
    {
        Console.Write("Qual o nome do ficheiro a criar:\t");
        string fileName = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(fileName))
        {
            writer.Write(output);
        }
    }
}

bool isNumber(String variavel)
{
    return int.TryParse(variavel, out int i);
}