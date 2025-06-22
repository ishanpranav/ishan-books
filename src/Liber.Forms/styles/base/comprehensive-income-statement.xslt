<?xml version="1.0" encoding="utf-8"?>
<!--
comprehensive-income-statement.xslt
Copyright (c) 2023-2025 Ishan Pranav. All rights reserved.
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
    <xsl:key name="lines-by-account" match="line[ancestor::transaction[other-equity = 'true']]" use="account"/>
    <xsl:variable name="complexTransactions" select="count(//transaction[other-equity = 'true' and count(line) &gt; 2])"/>
    <xsl:template name="comprehensive-income-statement">
        <xsl:param name="title"/>
        <xsl:if test="$complexTransactions &gt; 0">
            <dialog id="warningDialog" open="open">
                Warning! The general journal contains <xsl:value-of select="$complexTransactions"/> complex adjustments. For transactions that impact other comprehensive income or loss, please ensure that there is exactly one debit account and exactly one credit account.
                <button id="closeButton" onclick="document.getElementById('warningDialog').close()">Close</button>
            </dialog>
        </xsl:if>
        <table>
            <thead>
                <tr>
                    <th colspan="2" class="subtitle">
                        <xsl:value-of select="company/name"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="title">
                        <xsl:value-of select="$title"/>
                    </th>
                </tr>
                <tr>
                    <th colspan="2" class="bar dateline">
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
            <xsl:variable name="otherComprehensiveIncome" select="sum(company/account[other-equity = 'true']/balance) - sum(company/account[other-equity = 'true']/previous)"/>
            <tbody>
                <tr>
                    <th class="in-1 left">
                        <xsl:value-of select="liber:pngets('net-income', -$netIncome)"/>
                    </th>
                    <th class="right">
                        <xsl:value-of select="liber:fm(-$netIncome)"/>
                    </th>
                </tr>
                <xsl:for-each select="//line[other-equity = 'false' and ancestor::transaction[other-equity = 'true'] and generate-id() = generate-id(key('lines-by-account', account)[1])]">
                    <xsl:sort select="account" data-type="text" order="ascending"/>
                    <tr>
                        <td class="in-2 left account">
                            <xsl:value-of select="account"/>
                        </td>
                        <td class="right">
                            <xsl:variable name="lines-for-account" select="key('lines-by-account', account)"/>
                            <xsl:value-of select="liber:fm(sum($lines-for-account/debit) - sum($lines-for-account/credit))"/>
                        </td>
                    </tr>
                </xsl:for-each>
                <tr>
                    <th class="in-1 left">
                        <xsl:value-of select="liber:pngets('other-comprehensive-income', -$otherComprehensiveIncome)"/>
                    </th>
                    <th class="right">
                        <xsl:value-of select="liber:fm(-$otherComprehensiveIncome)"/>
                    </th>
                </tr>
                <tr>
                    <th class="left">
                        <xsl:value-of select="liber:pngets('comprehensive-income', -($netIncome + $otherComprehensiveIncome))"/>
                    </th>
                    <td class="total right">
                        <xsl:value-of select="liber:fm(-($netIncome + $otherComprehensiveIncome))"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
