<?xml version="1.0" encoding="utf-8"?>
<!--
balance-sheet.xslt
Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
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
    <xsl:template name="balance-sheet">
        <xsl:param name="comparative" select="false"/>
        <xsl:variable name="bank" select="sum(company/account[type = 'Bank']/balance)"/>
        <xsl:variable name="bank2" select="sum(company/account[type = 'Bank']/previous)"/>
        <xsl:variable name="otherCurrentAsset" select="sum(company/account[type = 'OtherCurrentAsset']/balance)"/>
        <xsl:variable name="otherCurrentAsset2" select="sum(company/account[type = 'OtherCurrentAsset']/previous)"/>
        <xsl:variable name="fixedAsset" select="sum(company/account[type = 'FixedAsset']/balance)"/>
        <xsl:variable name="fixedAsset2" select="sum(company/account[type = 'FixedAsset']/previous)"/>
        <xsl:variable name="otherAsset" select="sum(company/account[type = 'OtherAsset']/balance)"/>
        <xsl:variable name="otherAsset2" select="sum(company/account[type = 'OtherAsset']/previous)"/>
        <xsl:variable name="creditCard" select="sum(company/account[type = 'CreditCard']/balance)"/>
        <xsl:variable name="creditCard2" select="sum(company/account[type = 'CreditCard']/previous)"/>
        <xsl:variable name="otherCurrentLiability" select="sum(company/account[type = 'OtherCurrentLiability']/balance)"/>
        <xsl:variable name="otherCurrentLiability2" select="sum(company/account[type = 'OtherCurrentLiability']/previous)"/>
        <xsl:variable name="longTermLiability" select="sum(company/account[type = 'LongTermLiability']/balance)"/>
        <xsl:variable name="longTermLiability2" select="sum(company/account[type = 'LongTermLiability']/previous)"/>
        <xsl:variable name="equity" select="sum(company/account[type = 'Equity']/balance)"/>
        <xsl:variable name="equity2" select="sum(company/account[type = 'Equity']/previous)"/>
        <tbody>
            <tr>
                <th class="left">
                    <xsl:value-of select="liber:gets('assets')"/>
                </th>
            </tr>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('current-assets')"/>
                </th>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'Bank'"/>
                <xsl:with-param name="balance" select="$bank"/>
                <xsl:with-param name="previous" select="$bank2"/>
                <xsl:with-param name="indent" select="2"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'OtherCurrentAsset'"/>
                <xsl:with-param name="balance" select="$otherCurrentAsset"/>
                <xsl:with-param name="previous" select="$otherCurrentAsset2"/>
                <xsl:with-param name="indent" select="2"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('total-current-assets')"/>
                </th>
                <td class="subtotal right">
                    <xsl:value-of select="liber:fm($bank + $otherCurrentAsset)"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($bank2 + $otherCurrentAsset2)"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('non-current-assets')"/>
                </th>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'FixedAsset'"/>
                <xsl:with-param name="balance" select="$fixedAsset"/>
                <xsl:with-param name="previous" select="$fixedAsset2"/>
                <xsl:with-param name="indent" select="2"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'OtherAsset'"/>
                <xsl:with-param name="balance" select="$otherAsset"/>
                <xsl:with-param name="previous" select="$otherAsset2"/>
                <xsl:with-param name="indent" select="2"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('total-non-current-assets')"/>
                </th>
                <td class="subtotal right">
                    <xsl:value-of select="liber:fm($fixedAsset + $otherAsset)"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($fixedAsset2 + $otherAsset2)"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
            <tr>
                <th class="left">
                    <xsl:value-of select="liber:gets('total-assets')"/>
                </th>
                <td class="total right">
                    <xsl:value-of select="liber:fm($bank + $otherCurrentAsset + $fixedAsset + $otherAsset)"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="total right">
                            <xsl:value-of select="liber:fm($bank2 + $otherCurrentAsset2 + $fixedAsset2 + $otherAsset2)"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
            <tr></tr>
            <tr>
                <th class="left">
                    <xsl:value-of select="liber:fgets('liabilities-equity{0}', liber:pngets(company/type, -$equity2))"/>
                </th>
            </tr>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('liabilities')"/>
                </th>
            </tr>
            <tr>
                <th class="in-2 left">
                    <xsl:value-of select="liber:gets('current-liabilities')"/>
                </th>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'CreditCard'"/>
                <xsl:with-param name="balance" select="-$creditCard"/>
                <xsl:with-param name="previous" select="-$creditCard2"/>
                <xsl:with-param name="indent" select="3"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'OtherCurrentLiability'"/>
                <xsl:with-param name="balance" select="-$otherCurrentLiability"/>
                <xsl:with-param name="previous" select="-$otherCurrentLiability2"/>
                <xsl:with-param name="indent" select="3"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <tr>
                <th class="in-2 left">
                    <xsl:value-of select="liber:gets('total-current-liabilities')"/>
                </th>
                <td class="subtotal right">
                    <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability))"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2))"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'LongTermLiability'"/>
                <xsl:with-param name="balance" select="-$longTermLiability"/>
                <xsl:with-param name="previous" select="-$longTermLiability2"/>
                <xsl:with-param name="indent" select="2"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <tr>
                <th class="in-1 left">
                    <xsl:value-of select="liber:gets('total-liabilities')"/>
                </th>
                <td class="subtotal right">
                    <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability))"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2))"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type" select="'Equity'"/>
                <xsl:with-param name="balance" select="-$equity"/>
                <xsl:with-param name="previous" select="-$equity2"/>
                <xsl:with-param name="indent" select="1"/>
                <xsl:with-param name="title" select="liber:fgets('{0}', liber:pngets(company/type, -$equity2))"/>
                <xsl:with-param name="subtitle" select="liber:fgets('total-equity{0}', liber:pngets(company/type, -$equity2))"/>
                <xsl:with-param name="comparative" select="$comparative"/>
            </xsl:apply-templates>
            <tr>
                <th class="left">
                    <xsl:value-of select="liber:fgets('total-liabilities-equity{0}', liber:pngets(company/type, -$equity2))"/>
                </th>
                <td class="total right">
                    <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability + $equity))"/>
                </td>
                <xsl:choose>
                    <xsl:when test="$comparative = 1">
                        <td class="total right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2 + $equity2))"/>
                        </td>
                    </xsl:when>
                </xsl:choose>
            </tr>
        </tbody>
    </xsl:template>
    <xsl:template match="/report">
        <xsl:call-template name="financial-statement">
            <xsl:with-param name="title" select="title"/>
            <xsl:with-param name="table">
                <thead>
                    <tr>
                        <th colspan="2" class="subtitle">
                            <xsl:value-of select="company/name"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="title">
                            <xsl:value-of select="title"/>
                        </th>
                    </tr>
                    <tr>
                        <th colspan="2" class="bar dateline">
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
                <xsl:call-template name="balance-sheet"/>
            </xsl:with-param>
        </xsl:call-template>
    </xsl:template>
</xsl:stylesheet>
