<h1 align="center">Windows Activation Tool - Educational Purposes Only</h1>


Descrição

Este repositório contém um código em C# que simula um ativador de Windows. Este projeto foi desenvolvido exclusivamente para fins educacionais e demonstra como ativadores de terceiros podem expor o sistema a riscos de segurança.

Objetivo

Este projeto visa mostrar os perigos dos ativadores como KMSPico, exemplificando como esses programas podem conceder acesso indesejado ao sistema. O código cria uma conexão reversa e envia um e-mail com os dados de IP da máquina, ilustrando o perigo real que muitos usuários desconhecem.

Pré-requisitos e Configuração

Para utilizar e testar o código, algumas configurações específicas são necessárias:

	1.	Configuração do Payload:
	•	No método ExecutaPayloadnaMemory, é necessário configurar o array de bytes (byte[] buf) para o payload desejado. Esse payload é responsável por iniciar a shell reversa.
	2.	Configuração do Envio de E-mail:
	•	No método SendEmailAsync, configure os seguintes campos:
	•	fromAddress: Endereço de e-mail que enviará a notificação.
	•	toAddress: Endereço de e-mail do destinatário (o atacante) que receberá os dados.
	•	smtpServer, smtpUsername, e smtpPassword: Configurações do servidor SMTP, necessárias para o envio do e-mail. No exemplo, é utilizado o SendGrid, mas pode ser ajustado para outro serviço.
	•	O e-mail enviado inclui informações como o IP local da máquina.
	3.	Interface Form1:
	•	É necessário configurar a interface Form1 com a aparência demonstrada, incluindo um botão para ativação e uma label para exibir informações sobre o sistema operacional.

Como usar

	1.	Clone o repositório e abra o projeto no Visual Studio.
	2.	Configure o Form1, o payload e as informações de e-mail conforme descrito acima.
	3.	Compile e execute o código em um ambiente seguro e isolado.
	4.	Explore o código para entender como a conexão reversa é estabelecida e os riscos envolvidos.

Aviso

	•	Este código NÃO deve ser usado em ambientes de produção.
	•	Não utilize para ativação real do Windows.
	•	O uso deve ser estritamente para estudo e conscientização sobre os riscos.
	•	Não nos responsabilizamos pelo uso indevido deste código.

Contribuição

Para sugestões de melhoria, abra um Pull Request ou issue.

A demonstração desse "Ativador" pode ser vista nesse vídeo: https://youtu.be/sEU1CfOV-LA
