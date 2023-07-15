<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:msxsl="urn:schemas-microsoft-com:xslt"
  exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template name="date-year">
		<xsl:param name="value"/>
		<xsl:value-of select="substring($value, 1, 4)"/>
	</xsl:template>
	<xsl:template name="date-long">
		<xsl:param name="value"/>
		<xsl:variable name="month" select="substring($value, 6, 2)"/>
		<xsl:choose>
			<xsl:when test="$month = '01'">January</xsl:when>
			<xsl:when test="$month = '02'">February</xsl:when>
			<xsl:when test="$month = '03'">March</xsl:when>
			<xsl:when test="$month = '04'">April</xsl:when>
			<xsl:when test="$month = '05'">May</xsl:when>
			<xsl:when test="$month = '06'">June</xsl:when>
			<xsl:when test="$month = '07'">July</xsl:when>
			<xsl:when test="$month = '08'">August</xsl:when>
			<xsl:when test="$month = '09'">September</xsl:when>
			<xsl:when test="$month = '10'">October</xsl:when>
			<xsl:when test="$month = '11'">November</xsl:when>
			<xsl:when test="$month = '12'">December</xsl:when>
		</xsl:choose>
		<xsl:text> </xsl:text>
		<xsl:value-of select="format-number((substring($value, 9, 2)), '#')"/>
		<xsl:text>, </xsl:text>
		<xsl:call-template name="date-year">
			<xsl:with-param name="value" select="$value"/>
		</xsl:call-template>
	</xsl:template>
	<xsl:template name="number">
		<xsl:param name="value"/>
		<xsl:choose>
			<xsl:when test="$value = 0">-</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="format-number($value, ' #,##0.00 ;(#,##0.00)')"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:template>
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
					margin: 0.75in auto 0.75in auto;
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
					border-bottom: 1px solid;
					font-weight: bold;
					}

					.total {
					border-top: 1px solid;
					border-bottom: 3px double;
					font-weight: bold;
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
