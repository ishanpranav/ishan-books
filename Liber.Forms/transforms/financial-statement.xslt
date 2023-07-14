<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template name="financial-statement">
		<xsl:param name="title"/>
		<xsl:param name="table"/>
		<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
			<head>
				<meta charset="utf-8"/>
				<title>
					<xsl:value-of select="$title"/>
				</title>
				<style type="text/css">
					table {
					font-family: Arial;
					font-size: 11pt;
					}

					th {
					font-weight: bold;
					}

					.bar {
					border-bottom: 3px solid;
					padding-bottom: 1px;
					}

					.title {
					font-size: 14pt;
					}

					.heading {
					font-weight: bold;
					border-bottom: 1px solid;
					}

					.total {
					font-weight: bold;
					border-top: 1px solid;
					border-bottom: 2px solid double;
					}

					.left {
					text-align: left;
					}

					.right {
					text-align: right;
					}
				</style>
			</head>
			<body>
				<table>
					<xsl:copy-of select="$table"/>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
