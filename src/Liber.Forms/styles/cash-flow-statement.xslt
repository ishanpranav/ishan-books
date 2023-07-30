<?xml version="1.0" encoding="utf-8"?>
<!--
cash-flow-statement.xslt
Copyright (c) 2023 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:include href="base/financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template match="/report">
        <xsl:variable name="title" select="liber:gets('cash-flow-statement')"/>
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="styles">
                <link rel="stylesheet" type="text/css" href="https://liber.example/styles/cash-flow-statement.css"/>
            </xsl:with-param>
            <xsl:with-param name="table">
                <colgroup>
                    <col class="wide"></col>
                </colgroup>
                <thead>
                    <tr>
                        <th colspan="2">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="$title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="bar">
                            <xsl:value-of select="liber:ftspanl(started, posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:ftspans(started, posted)"/>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="netIncome" select="sum(company/account[type = 'Income' or type = 'Cost' or type = 'Expense' or type = 'OtherIncomeExpense' or type ='IncomeTaxExpense']/balance)"/>
                <xsl:variable name="operations" select="sum(company/account[(type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'OtherIncomeExpense')]/previous) - sum(company/account[(type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'OtherIncomeExpense')]/balance)"/>
                <tbody>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(started))"/>
                        </th>
                        <th class="subtotal right">
                            <xsl:value-of select="liber:fm(sum(company/account[type = 'Bank']/balance))"/>
                        </th>
                    </tr>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('operations')"/>
                        </th>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                        </td>
                        <td class="right">
                            <xsl:value-of select="liber:fm(-$netIncome)"/>
                        </td>
                    </tr>
                    <tr>
                        <td class="in-2 left">
                            <xsl:value-of select="liber:fgets('adjustments{0}{1}', liber:pngets('net-income', -$netIncome), liber:pngets('operations', $operations))" />
                        </td>
                    </tr>
                    <xsl:for-each select="company/account[(type = 'OtherCurrentAsset' or type = 'OtherCurrentLiability' or type = 'OtherIncomeExpense') and (previous - balance != 0)]">
                        <tr>
                            <td class="in-4 left">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(previous - balance)"/>
                            </td>
                        </tr>
                    </xsl:for-each>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:pngets('operations', $operations)"/>
                        </th>
                        <th class="subtotal right">
                            <xsl:value-of select="liber:fm($operations)"/>
                        </th>
                    </tr>
                </tbody>
                <tfoot>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(posted))"/>
                        </th>
                        <th class="total right">
                            <xsl:value-of select="liber:fm(sum(company/account[type = 'Bank']/previous))"/>
                        </th>
                    </tr>
                </tfoot>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
