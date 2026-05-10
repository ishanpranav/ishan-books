<?xml version="1.0" encoding="utf-8"?>
<!--
financial-statement.xslt
Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:key name="accounts-by-type-and-name" match="account" use="concat(type, '|', name)"/>
    <xsl:variable name="netIncome" select="sum(report/company/account[type = 'Income' or type = 'Cost' or type = 'Expense' or type = 'OtherIncomeExpense' or type ='IncomeTaxExpense']/balance)"/>
    <xsl:template name="financial-statement">
        <xsl:param name="table"/>
        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta charset="utf-8"/>
                <title>
                    <xsl:value-of select="title"/>
                </title>
                <link rel="stylesheet" type="text/css" href="https://liber.example/styles/financial-statement.css"/>
            </head>
            <body>
                <table>
                    <xsl:copy-of select="$table"/>
                </table>
            </body>
        </html>
    </xsl:template>
    <xsl:template match="company">
        <xsl:param name="type"/>
        <xsl:param name="balance"/>
        <xsl:param name="previous" select="0"/>
        <xsl:param name="indent"/>
        <xsl:param name="totalIndent" select="$indent"/>
        <xsl:param name="title" select="liber:gets($type)"/>
        <xsl:param name="subtitle" select="liber:pngets($type, $balance)"/>
        <xsl:param name="comparative" select="0"/>
        <xsl:param name="cols" select="2"/>
        <xsl:param name="absoluteSize" select="1"/>
        <xsl:param name="commonSize" select="0"/>
        <xsl:param name="denominator" select="0"/>
        <xsl:param name="denominator2" select="0"/>
        <xsl:choose>
            <xsl:when test="$balance != 0 or $previous != 0">
                <xsl:if test="detail = 'true'">
                    <tr>
                        <td class="in-{$indent} left" colspan="{$cols}">
                            <xsl:value-of select="concat($title, ':')"/>
                        </td>
                    </tr>
                    <xsl:for-each select="//account[
                        type = $type
                        and generate-id() = generate-id(key('accounts-by-type-and-name', concat(type, '|', name))[1])
                    ]">
                        <xsl:variable name="group"         select="key('accounts-by-type-and-name', concat(type, '|', name))"/>
                        <xsl:variable name="groupBalance"  select="sum($group/balance)"/>
                        <xsl:variable name="groupPrevious" select="sum($group/previous)"/>
                        <xsl:if test="$groupBalance != 0 or ($comparative = 1 and $groupPrevious != 0)">
                            <tr>
                                <td class="in-{$indent + 1} left account">
                                    <xsl:value-of select="name"/>
                                </td>
                                <xsl:if test="$absoluteSize = 1">
                                    <td class="right">
                                        <xsl:value-of select="liber:fm($type, $groupBalance)"/>
                                    </td>
                                </xsl:if>
                                <xsl:if test="$commonSize = 1">
                                    <td class="right">
                                        <xsl:value-of select="liber:fp($type, $groupBalance, $denominator)"/>
                                    </td>
                                </xsl:if>
                                <xsl:if test="$comparative = 1">
                                    <xsl:if test="$absoluteSize = 1">
                                        <td class="right">
                                            <xsl:value-of select="liber:fm($type, $groupPrevious)"/>
                                        </td>
                                    </xsl:if>
                                    <xsl:if test="$commonSize = 1">
                                        <td class="right">
                                            <xsl:value-of select="liber:fp($type, $groupPrevious, $denominator2)"/>
                                        </td>
                                    </xsl:if>
                                </xsl:if>
                            </tr>
                        </xsl:if>
                    </xsl:for-each>
                </xsl:if>
                <tr>
                    <xsl:choose>
                        <xsl:when test="detail = 'false'">
                            <td class="in-{$indent} left">
                                <xsl:value-of select="$title"/>
                            </td>
                        </xsl:when>
                        <xsl:when test="detail = 'true'">
                            <td class="in-{$totalIndent + 2} left">
                                <xsl:value-of select="$subtitle"/>
                            </td>
                        </xsl:when>
                    </xsl:choose>
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($balance)"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp($balance div $denominator)"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$comparative = 1">
                        <xsl:if test="$absoluteSize = 1">
                            <td class="subtotal right">
                                <xsl:value-of select="liber:fm($previous)"/>
                            </td>
                        </xsl:if>
                        <xsl:if test="$commonSize = 1">
                            <td class="subtotal right">
                                <xsl:value-of select="liber:fp($previous div $denominator2)"/>
                            </td>
                        </xsl:if>
                    </xsl:if>
                </tr>
            </xsl:when>
        </xsl:choose>
    </xsl:template>
</xsl:stylesheet>
