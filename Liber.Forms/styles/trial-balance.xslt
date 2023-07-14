<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    exclude-result-prefixes="msxsl">
	<xsl:output method="html" indent="yes"/>
	<xsl:template match="/report">
		<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
			<head>
				<meta charset="utf-8"/>
				<title>Trial Balance</title>
				<style>
					.statement {
						font-family: Arial;
						font-size: 11pt;
					}
					
						.statement th {
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
					
					.left {
						text-align: left;
					}
				</style>
			</head>
			<body>
				<table class="statement">
					<thead>
						<tr>
							<th colspan="3">
								<xsl:value-of select="company/name"/>
							</th>
						</tr>
						<tr>
							<th colspan="3" class="title">Trial Balance</th>
						</tr>
						<tr>
							<th colspan="3" class="bar">
								<xsl:value-of select="end"/>
							</th>
						</tr>
					</thead>
					<tbody>
						<tr>
							<th></th>
							<th colspan="2" class="heading">
								<xsl:value-of select="end"/>
							</th>
						</tr>
						<tr>
							<th></th>
							<th class="heading">Debit</th>
							<th class="heading">Credit</th>
						</tr>
						<xsl:for-each select="company/account">
							<tr>
								<td class="left">
									<xsl:value-of select="name"/>
								</td>
								<td>
									<xsl:choose>
										<xsl:when test="debit = 0">
											-
										</xsl:when>
										<xsl:when test="debit &gt; 0">
											<xsl:value-of select="debit"/>
										</xsl:when>
									</xsl:choose>
								</td>
								<td>
									<xsl:choose>
										<xsl:when test="debit = 0">
											-
										</xsl:when>
										<xsl:when test="debit &lt; 0">
											<xsl:value-of select="-debit"/>
										</xsl:when>
									</xsl:choose>
								</td>
							</tr>
						</xsl:for-each>
					</tbody>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
