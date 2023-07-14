<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet
	version="1.0"
	xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
	xmlns:html="http://www.w3.org/1999/xhtml"
    exclude-result-prefixes="msxsl">
	<xsl:include href="financial-statement.xslt"/>
	<xsl:output method="html" indent="yes"/>
	<xsl:variable name="title">Trial Balance</xsl:variable>
	<xsl:template match="/report">
		<xsl:call-template name="financial-statement">
			<xsl:with-param name="title" select="$title"/>
			<xsl:with-param name="table">
				<thead>
					<tr>
						<th colspan="3">
							<xsl:value-of select="company/name"/>
						</th>
					</tr>
					<tr>
						<th colspan="3" class="title">
							<xsl:value-of select="$title"/>
						</th>
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
							<td class="right">
								<xsl:value-of select="format-number(debit, ' #,##0.00 ;(#,##0.00)')"/>
							</td>
							<td class="right">
								<xsl:value-of select="format-number(credit, ' #,##0.00 ;(#,##0.00)')"/>
							</td>
						</tr>
					</xsl:for-each>
				</tbody>
				<tfoot>
					<tr>
						<td></td>
						<td class="total right">
							<xsl:value-of select="format-number(sum(company/account/debit), ' #,##0.00 ;(#,##0.00)')"/>
						</td>
						<td class="total right">
							<xsl:value-of select="format-number(sum(company/account/credit), ' #,##0.00 ;(#,##0.00)')"/>
						</td>
					</tr>
				</tfoot>
			</xsl:with-param>
		</xsl:call-template>
	</xsl:template>
</xsl:stylesheet>
