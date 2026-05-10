<?xml version="1.0" encoding="utf-8"?>
<!--
balance-sheet.xslt
Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet
    version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:html="http://www.w3.org/1999/xhtml"
    xmlns:liber="urn:liber"
    exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="balance-sheet-internal" match="/report">
        <xsl:param name="comparative" select="0"/>
        <xsl:param name="absoluteSize" select="1"/>
        <xsl:param name="commonSize" select="0"/>
        <xsl:variable name="bank"                   select="sum(company/account[type = 'Bank']/balance)"/>
        <xsl:variable name="bank2"                  select="sum(company/account[type = 'Bank']/previous)"/>
        <xsl:variable name="otherCurrentAsset"      select="sum(company/account[type = 'OtherCurrentAsset']/balance)"/>
        <xsl:variable name="otherCurrentAsset2"     select="sum(company/account[type = 'OtherCurrentAsset']/previous)"/>
        <xsl:variable name="fixedAsset"             select="sum(company/account[type = 'FixedAsset']/balance)"/>
        <xsl:variable name="fixedAsset2"            select="sum(company/account[type = 'FixedAsset']/previous)"/>
        <xsl:variable name="otherAsset"             select="sum(company/account[type = 'OtherAsset']/balance)"/>
        <xsl:variable name="otherAsset2"            select="sum(company/account[type = 'OtherAsset']/previous)"/>
        <xsl:variable name="creditCard"             select="sum(company/account[type = 'CreditCard']/balance)"/>
        <xsl:variable name="creditCard2"            select="sum(company/account[type = 'CreditCard']/previous)"/>
        <xsl:variable name="otherCurrentLiability"  select="sum(company/account[type = 'OtherCurrentLiability']/balance)"/>
        <xsl:variable name="otherCurrentLiability2" select="sum(company/account[type = 'OtherCurrentLiability']/previous)"/>
        <xsl:variable name="longTermLiability"      select="sum(company/account[type = 'LongTermLiability']/balance)"/>
        <xsl:variable name="longTermLiability2"     select="sum(company/account[type = 'LongTermLiability']/previous)"/>
        <xsl:variable name="equity"                 select="sum(company/account[type = 'Equity']/balance)"/>
        <xsl:variable name="equity2"                select="sum(company/account[type = 'Equity']/previous)"/>
        <xsl:variable name="totalAssets"            select="$bank + $otherCurrentAsset + $fixedAsset + $otherAsset"/>
        <xsl:variable name="totalAssets2"           select="$bank2 + $otherCurrentAsset2 + $fixedAsset2 + $otherAsset2"/>
        <xsl:variable name="perPeriod">
            <xsl:choose>
                <xsl:when test="$absoluteSize = 1 and $commonSize = 1">2</xsl:when>
                <xsl:otherwise>1</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="cols">
            <xsl:choose>
                <xsl:when test="$comparative = 1">
                    <xsl:value-of select="1 + (2 * $perPeriod)"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="1 + $perPeriod"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <tbody>
            <tr>
                <th class="left" colspan="{$cols}">
                    <xsl:value-of select="liber:gets('assets')"/>
                </th>
            </tr>
            <xsl:if test="$bank + $otherCurrentAsset != 0 or ($comparative = 1 and $bank2 + $otherCurrentAsset2 != 0)">
                <tr>
                    <td class="left" colspan="{$cols}">
                        <xsl:value-of select="concat(liber:gets('current-assets'), ':')"/>
                    </td>
                </tr>
            </xsl:if>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'Bank'"/>
                <xsl:with-param name="balance"      select="$bank"/>
                <xsl:with-param name="previous"     select="$bank2"/>
                <xsl:with-param name="indent"       select="1"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'OtherCurrentAsset'"/>
                <xsl:with-param name="balance"      select="$otherCurrentAsset"/>
                <xsl:with-param name="previous"     select="$otherCurrentAsset2"/>
                <xsl:with-param name="indent"       select="1"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <tr>
                <td class="in-4 left">
                    <xsl:value-of select="liber:gets('total-current-assets')"/>
                </td>
                <xsl:if test="$absoluteSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm($bank + $otherCurrentAsset)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$commonSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fp(($bank + $otherCurrentAsset) div $totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$comparative = 1">
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm($bank2 + $otherCurrentAsset2)"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(($bank2 + $otherCurrentAsset2) div $totalAssets2)"/>
                        </td>
                    </xsl:if>
                </xsl:if>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'FixedAsset'"/>
                <xsl:with-param name="balance"      select="$fixedAsset"/>
                <xsl:with-param name="previous"     select="$fixedAsset2"/>
                <xsl:with-param name="indent"       select="0"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'OtherAsset'"/>
                <xsl:with-param name="balance"      select="$otherAsset"/>
                <xsl:with-param name="previous"     select="$otherAsset2"/>
                <xsl:with-param name="indent"       select="0"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <tr>
                <td class="in-5 left">
                    <xsl:value-of select="liber:gets('total-assets')"/>
                </td>
                <xsl:if test="$absoluteSize = 1">
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fm($totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$commonSize = 1">
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fp($totalAssets div $totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$comparative = 1">
                    <xsl:if test="$absoluteSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fm($totalAssets2)"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fp($totalAssets2 div $totalAssets2)"/>
                        </td>
                    </xsl:if>
                </xsl:if>
            </tr>
            <tr>
                <th class="left" colspan="{$cols}">
                    <xsl:value-of select="liber:fgets('liabilities-equity{0}', liber:pngets(company/type, -$equity2))"/>
                </th>
            </tr>
            <xsl:if test="$creditCard + $otherCurrentLiability != 0 or ($comparative = 1 and $creditCard2 + $otherCurrentLiability2 != 0)">
                <tr>
                    <td class="left" colspan="{$cols}">
                        <xsl:value-of select="concat(liber:gets('current-liabilities'), ':')"/>
                    </td>
                </tr>
            </xsl:if>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'CreditCard'"/>
                <xsl:with-param name="balance"      select="-$creditCard"/>
                <xsl:with-param name="previous"     select="-$creditCard2"/>
                <xsl:with-param name="indent"       select="1"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'OtherCurrentLiability'"/>
                <xsl:with-param name="balance"      select="-$otherCurrentLiability"/>
                <xsl:with-param name="previous"     select="-$otherCurrentLiability2"/>
                <xsl:with-param name="indent"       select="1"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <tr>
                <td class="in-4 left">
                    <xsl:value-of select="liber:gets('total-current-liabilities')"/>
                </td>
                <xsl:if test="$absoluteSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability))"/>
                    </td>
                </xsl:if>
                <xsl:if test="$commonSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fp(-($creditCard + $otherCurrentLiability) div $totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$comparative = 1">
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(-($creditCard2 + $otherCurrentLiability2) div $totalAssets2)"/>
                        </td>
                    </xsl:if>
                </xsl:if>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'LongTermLiability'"/>
                <xsl:with-param name="balance"      select="-$longTermLiability"/>
                <xsl:with-param name="previous"     select="-$longTermLiability2"/>
                <xsl:with-param name="indent"       select="0"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <tr>
                <td class="in-4 left">
                    <xsl:value-of select="liber:gets('total-liabilities')"/>
                </td>
                <xsl:if test="$absoluteSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability))"/>
                    </td>
                </xsl:if>
                <xsl:if test="$commonSize = 1">
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fp(-($creditCard + $otherCurrentLiability + $longTermLiability) div $totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$comparative = 1">
                    <xsl:if test="$absoluteSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="subtotal right">
                            <xsl:value-of select="liber:fp(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2) div $totalAssets2)"/>
                        </td>
                    </xsl:if>
                </xsl:if>
            </tr>
            <xsl:apply-templates select="company">
                <xsl:with-param name="type"         select="'Equity'"/>
                <xsl:with-param name="balance"      select="-$equity"/>
                <xsl:with-param name="previous"     select="-$equity2"/>
                <xsl:with-param name="indent"       select="0"/>
                <xsl:with-param name="totalIndent"  select="2"/>
                <xsl:with-param name="title"        select="liber:fgets('{0}', liber:pngets(company/type, -$equity2))"/>
                <xsl:with-param name="subtitle"     select="liber:fgets('total-equity{0}', liber:pngets(company/type, -$equity2))"/>
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="cols"         select="$cols"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
                <xsl:with-param name="denominator"  select="$totalAssets"/>
                <xsl:with-param name="denominator2" select="$totalAssets2"/>
            </xsl:apply-templates>
            <tr>
                <td class="in-5 left">
                    <xsl:value-of select="liber:fgets('total-liabilities-equity{0}', liber:pngets(company/type, -$equity2))"/>
                </td>
                <xsl:if test="$absoluteSize = 1">
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fm(-($creditCard + $otherCurrentLiability + $longTermLiability + $equity))"/>
                    </td>
                </xsl:if>
                <xsl:if test="$commonSize = 1">
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fp(-($creditCard + $otherCurrentLiability + $longTermLiability + $equity) div $totalAssets)"/>
                    </td>
                </xsl:if>
                <xsl:if test="$comparative = 1">
                    <xsl:if test="$absoluteSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fm(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2 + $equity2))"/>
                        </td>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <td class="grand-total right">
                            <xsl:value-of select="liber:fp(-($creditCard2 + $otherCurrentLiability2 + $longTermLiability2 + $equity2) div $totalAssets2)"/>
                        </td>
                    </xsl:if>
                </xsl:if>
            </tr>
        </tbody>
    </xsl:template>
    <xsl:template name="balance-sheet">
        <xsl:param name="title"/>
        <xsl:param name="comparative" select="0"/>
        <xsl:param name="absoluteSize" select="1"/>
        <xsl:param name="commonSize" select="0"/>
        <xsl:variable name="perPeriod">
            <xsl:choose>
                <xsl:when test="$absoluteSize = 1 and $commonSize = 1">2</xsl:when>
                <xsl:otherwise>1</xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <xsl:variable name="cols">
            <xsl:choose>
                <xsl:when test="$comparative = 1">
                    <xsl:value-of select="1 + (2 * $perPeriod)"/>
                </xsl:when>
                <xsl:otherwise>
                    <xsl:value-of select="1 + $perPeriod"/>
                </xsl:otherwise>
            </xsl:choose>
        </xsl:variable>
        <table>
            <thead>
                <tr>
                    <th colspan="{$cols}" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="{$cols}" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="{$cols}" class="dateline">
                        <xsl:value-of select="liber:fdatel(posted)"/>
                    </th>
                </tr>
                <xsl:if test="company/multiple != ''">
                    <tr>
                        <th colspan="{$cols}" class="dateline">
                            <xsl:value-of select="company/multiple"/>
                        </th>
                    </tr>
                </xsl:if>
                <tr class="overline">
                    <th/>
                    <xsl:if test="$absoluteSize = 1">
                        <th class="heading">
                            <xsl:value-of select="liber:fdates(posted)"/>
                        </th>
                    </xsl:if>
                    <xsl:if test="$commonSize = 1">
                        <th class="heading">%</th>
                    </xsl:if>
                    <xsl:if test="$comparative = 1">
                        <xsl:if test="$absoluteSize = 1">
                            <th class="heading">
                                <xsl:value-of select="liber:fdates(started)"/>
                            </th>
                        </xsl:if>
                        <xsl:if test="$commonSize = 1">
                            <th class="heading">%</th>
                        </xsl:if>
                    </xsl:if>
                </tr>
            </thead>
            <xsl:call-template name="balance-sheet-internal">
                <xsl:with-param name="comparative"  select="$comparative"/>
                <xsl:with-param name="absoluteSize" select="$absoluteSize"/>
                <xsl:with-param name="commonSize"   select="$commonSize"/>
            </xsl:call-template>
        </table>
    </xsl:template>
</xsl:stylesheet>
