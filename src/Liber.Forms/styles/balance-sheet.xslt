<?xml version="1.0" encoding="utf-8"?>
<!--
balance-sheet.xslt
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
    <xsl:variable name="title" select="liber:gets('balance-sheet')"/>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="$title"/>
            <xsl:with-param name="table">
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
                            <xsl:value-of select="liber:fdatel(posted)"/>
                        </th>
                    </tr>
                    <tr>
                        <th></th>
                        <th class="heading">
                            <xsl:value-of select="liber:fdates(posted)"/>
                        </th>
                    </tr>
                </thead>
                <xsl:variable name="bank" select="sum(company/account[type = 'Bank']/balance)"/>
                <xsl:variable name="otherCurrentAsset" select="sum(company/account[type = 'OtherCurrentAsset']/balance)"/>
                <xsl:variable name="fixedAsset" select="sum(company/account[type = 'FixedAsset']/balance)"/>
                <xsl:variable name="otherAsset" select="sum(company/account[type = 'OtherAsset']/balance)"/>
                <xsl:variable name="creditCard" select="sum(company/account[type = 'CreditCard']/balance)"/>
                <xsl:variable name="otherCurrentLiability" select="sum(company/account[type = 'OtherCurrentLiability']/balance)"/>
                <xsl:variable name="longTermLiability" select="sum(company/account[type = 'LongTermLiability']/balance)"/>
                <xsl:variable name="equity" select="sum(company/account[type = 'Equity']/balance)"/>
                <tbody>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('assets')"/>
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('current-assets')"/>
                        </th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Bank</xsl:with-param>
                        <xsl:with-param name="balance" select="$bank"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherCurrentAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherCurrentAsset"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('total-current-assets')"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($bank + $otherCurrentAsset)"/>
                        </td>
                    </tr>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('non-current-assets')"/>
                        </th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">FixedAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$fixedAsset"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherAsset</xsl:with-param>
                        <xsl:with-param name="balance" select="$otherAsset"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('total-non-current-assets')"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($fixedAsset + $otherAsset)"/>
                        </td>
                    </tr>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:gets('total-assets')"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm($bank + $otherCurrentAsset + $fixedAsset + $otherAsset)"/>
                        </td>
                    </tr>
                    <tr></tr>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:fgets('liabilities-equity{0}', liber:gets(company/type))"/>
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('liabilities')"/>
                        </th>
                        <th></th>
                    </tr>
                    <tr>
                        <th class="in-2 left">
                            <xsl:value-of select="liber:gets('current-liabilities')"/>
                        </th>
                        <th></th>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">CreditCard</xsl:with-param>
                        <xsl:with-param name="balance" select="-$creditCard"/>
                        <xsl:with-param name="indent">3</xsl:with-param>
                    </xsl:apply-templates>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">OtherCurrentLiability</xsl:with-param>
                        <xsl:with-param name="balance" select="-$otherCurrentLiability"/>
                        <xsl:with-param name="indent">3</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-2 left">
                            <xsl:value-of select="liber:gets('total-current-liabilities')"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability))"/>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">LongTermLiability</xsl:with-param>
                        <xsl:with-param name="balance" select="-$longTermLiability"/>
                        <xsl:with-param name="indent">2</xsl:with-param>
                    </xsl:apply-templates>
                    <tr>
                        <th class="in-1 left">
                            <xsl:value-of select="liber:gets('total-liabilities')"/>
                        </th>
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability))"/>
                        </td>
                    </tr>
                    <xsl:apply-templates select="company">
                        <xsl:with-param name="type">Equity</xsl:with-param>
                        <xsl:with-param name="balance" select="-$equity"/>
                        <xsl:with-param name="indent">1</xsl:with-param>
                        <xsl:with-param name="title" select="liber:fgets('{0}', liber:gets(company/type))"/>
                        <xsl:with-param name="subtitle" select="liber:fgets('total-equity{0}', liber:gets(company/type))"/>
                    </xsl:apply-templates>
                    <tr>
                        <th class="left">
                            <xsl:value-of select="liber:fgets('total-liabilities-equity{0}', liber:gets(company/type))"/>
                        </th>
                        <td class="total right">
                            <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability + $equity))"/>
                        </td>
                    </tr>
                </tbody>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
