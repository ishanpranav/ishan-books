<!--   
cash-flow-statement.xslt
Copyright (c) 2023-2026 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
   -->
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:html="http://www.w3.org/1999/xhtml" xmlns:liber="urn:liber" version="1.0" exclude-result-prefixes="msxsl">
    <xsl:include href="financial-statement.xslt"/>
    <xsl:output method="html" indent="yes"/>
    <xsl:template name="cash-flow-statement">
        <xsl:param name="title"/>
        <xsl:variable name="workingCapital" select="sum(company/account[cash-flow = 'Operating']/previous) - sum(company/account[cash-flow = 'Operating']/balance)"/>
        <xsl:variable name="nonCash" select="sum(company/account[cash-flow = 'NonCash']/balance) - sum(company/account[cash-flow = 'NonCash']/previous)"/>
        <xsl:variable name="netGainLoss" select="sum(company/account[cash-flow = 'GainLoss']/balance)"/>
        <xsl:variable name="operating" select="-$netIncome + $workingCapital + $nonCash + $netGainLoss"/>
        <xsl:variable name="investing" select="sum(company/account[(cash-flow = 'Investing')]/previous) - sum(company/account[cash-flow = 'Investing']/balance) - $netGainLoss"/>
        <xsl:variable name="financing" select="sum(company/account[(cash-flow = 'Financing')]/previous) - sum(company/account[(cash-flow = 'Financing')]/balance)"/>
        <xsl:variable name="investingActivities" select="sum(company/account[cash-flow = 'Investing']/previous) - sum(company/account[cash-flow = 'Investing']/balance)"/>
        <xsl:variable name="netCashFlow" select="$operating + $investing + $financing"/>
        <xsl:variable name="cashPrevious" select="sum(company/account[cash-flow = 'Cash']/previous)"/>
        <xsl:variable name="cashBalance" select="sum(company/account[cash-flow = 'Cash']/balance)"/>
        <xsl:if test="($cashBalance - $cashPrevious - $netCashFlow) &gt;= 0.01 or ($cashBalance - $cashPrevious - $netCashFlow) &lt;= -0.01">
            <div class="warning">
                Warning! The sum of the beginning cash balance of <xsl:value-of select="liber:fm($cashPrevious)"/>, together with the net cash flow of <xsl:value-of select="liber:fm($netCashFlow)"/>, is <xsl:value-of select="liber:fm($cashPrevious + $netCashFlow)"/>, which does not reconcile with the ending cash balance of <xsl:value-of select="liber:fm($cashBalance)"/>. The excess cash of <xsl:value-of select="liber:fm($cashBalance - $cashPrevious - $netCashFlow)"/>, over the actual net cash flow of <xsl:value-of select="liber:fm($cashBalance - $cashPrevious)"/>, may be the result of misclassified accounts. Please ensure that all accounts are classified with the correct cash flow type.
        </div>
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
                    <th colspan="2" class="dateline">
                        <xsl:value-of select="liber:ftspanl(started, posted)"/>
                    </th>
                </tr>
                <xsl:if test="company/multiple != ''">
                    <tr>
                        <th colspan="2" class="dateline">
                            <xsl:value-of select="company/multiple"/>
                        </th>
                    </tr>
                </xsl:if>
                <tr class="overline">
                    <th/>
                    <th class="heading">
                        <xsl:value-of select="liber:ftspans(started, posted)"/>
                    </th>
                </tr>
            </thead>
            <tbody>
                <xsl:choose>
                    <xsl:when test="$operating != 0">
                        <tr>
                            <th class="left" colspan="2">
                                <xsl:value-of select="liber:gets('operating')"/>
                            </th>
                        </tr>
                        <tr>
                            <td class="left">
                                <xsl:value-of select="liber:gets('net-income')"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(-$netIncome)"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="left" colspan="2">
                                <xsl:value-of select="liber:gets('net-income-adjustments')"/>
                            </td>
                        </tr>
                        <xsl:choose>
                            <xsl:when test="company/detail = 'true'">
                                <xsl:if test="$workingCapital != 0">
                                    <tr>
                                        <td class="in-1 left">
                                            <xsl:value-of select="liber:gets('working-capital')"/>
                                        </td>
                                    </tr>
                                    <xsl:for-each select="company/account[cash-flow = 'Operating' and (previous - balance != 0)]">
                                        <tr>
                                            <td class="in-2 left account">
                                                <xsl:value-of select="name"/>
                                            </td>
                                            <td class="right">
                                                <xsl:value-of select="liber:fm(previous - balance)"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                    <tr>
                                        <td class="in-3 left">
                                            <xsl:value-of select="liber:pngets('working-capital', $workingCapital)"/>
                                        </td>
                                        <td class="subtotal right">
                                            <xsl:value-of select="liber:fm($workingCapital)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                                <xsl:if test="$nonCash != 0">
                                    <tr>
                                        <td class="in-1 left" colspan="2">
                                            <xsl:value-of select="liber:gets('non-cash')"/>
                                        </td>
                                    </tr>
                                    <xsl:for-each select="company/account[cash-flow = 'NonCash' and (balance - previous != 0)]">
                                        <tr>
                                            <td class="in-2 left account">
                                                <xsl:value-of select="name"/>
                                            </td>
                                            <td class="right">
                                                <xsl:value-of select="liber:fm(balance - previous)"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                    <tr>
                                        <td class="in-3 left">
                                            <xsl:value-of select="liber:pngets('non-cash', $nonCash)"/>
                                        </td>
                                        <td class="subtotal right">
                                            <xsl:value-of select="liber:fm($nonCash)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                                <xsl:if test="$netGainLoss != 0">
                                    <tr>
                                        <td class="in-1 left" colspan="2">
                                            <xsl:value-of select="liber:gets('less-gain')"/>
                                        </td>
                                    </tr>
                                    <xsl:for-each select="company/account[cash-flow = 'GainLoss' and balance != 0]">
                                        <tr>
                                            <td class="in-2 left account">
                                                <xsl:value-of select="name"/>
                                            </td>
                                            <td class="right">
                                                <xsl:value-of select="liber:fm(balance)"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                    <tr>
                                        <td class="in-3 left">
                                            <xsl:value-of select="liber:pngets('recognized-gain-loss', $netGainLoss)"/>
                                        </td>
                                        <td class="subtotal right">
                                            <xsl:value-of select="liber:fm($netGainLoss)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                            </xsl:when>
                            <xsl:otherwise>
                                <xsl:if test="$workingCapital != 0">
                                    <tr>
                                        <td class="in-1 left">
                                            <xsl:value-of select="liber:pngets('working-capital', $workingCapital)"/>
                                        </td>
                                        <td class="right">
                                            <xsl:value-of select="liber:fm($workingCapital)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                                <xsl:if test="$nonCash != 0">
                                    <tr>
                                        <td class="in-1 left">
                                            <xsl:value-of select="liber:pngets('non-cash', $nonCash)"/>
                                        </td>
                                        <td class="right">
                                            <xsl:value-of select="liber:fm($nonCash)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                                <xsl:if test="$netGainLoss != 0">
                                    <tr>
                                        <td class="in-1 left">
                                            <xsl:value-of select="liber:gets('less-gain')"/>
                                        </td>
                                        <td class="right">
                                            <xsl:value-of select="liber:fm($netGainLoss)"/>
                                        </td>
                                    </tr>
                                </xsl:if>
                            </xsl:otherwise>
                        </xsl:choose>
                    </xsl:when>
                </xsl:choose>
                <tr>
                    <td class="in-4 left">
                        <xsl:value-of select="liber:pngets('operating', $operating)"/>
                    </td>
                    <td class="total right">
                        <xsl:value-of select="liber:fm($operating)"/>
                    </td>
                </tr>
                <tr>
                    <th class="left" colspan="2">
                        <xsl:value-of select="liber:gets('investing')"/>
                    </th>
                </tr>
                <xsl:if test="$investing != 0">
                    <xsl:choose>
                        <xsl:when test="company/detail = 'true'">
                            <xsl:for-each select="company/account[cash-flow = 'Investing' and (previous - balance != 0)]">
                                <tr>
                                    <td class="left account">
                                        <xsl:value-of select="name"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm(previous - balance)"/>
                                    </td>
                                </tr>
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <xsl:if test="$investingActivities != 0">
                                <tr>
                                    <td class="left">
                                        <xsl:value-of select="liber:pngets('investing-activities', $investingActivities)"/>
                                    </td>
                                    <td class="right">
                                        <xsl:value-of select="liber:fm($investingActivities)"/>
                                    </td>
                                </tr>
                            </xsl:if>
                        </xsl:otherwise>
                    </xsl:choose>
                    <xsl:if test="$netGainLoss != 0">
                        <tr>
                            <td class="left">
                                <xsl:value-of select="liber:gets('plus-gain')"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(-$netGainLoss)"/>
                            </td>
                        </tr>
                    </xsl:if>
                </xsl:if>
                <tr>
                    <td class="in-4 left">
                        <xsl:value-of select="liber:pngets('investing', $investing)"/>
                    </td>
                    <td class="total right">
                        <xsl:value-of select="liber:fm($investing)"/>
                    </td>
                </tr>
                <tr>
                    <th class="left" colspan="2">
                        <xsl:value-of select="liber:gets('financing')"/>
                    </th>
                </tr>
                <xsl:if test="$financing != 0 and company/detail = 'true'">
                    <xsl:for-each select="company/account[cash-flow = 'Financing' and (previous - balance != 0)]">
                        <tr>
                            <td class="in-1 left account">
                                <xsl:value-of select="name"/>
                            </td>
                            <td class="right">
                                <xsl:value-of select="liber:fm(previous - balance)"/>
                            </td>
                        </tr>
                    </xsl:for-each>
                </xsl:if>
                <tr>
                    <td class="in-4 left">
                        <xsl:value-of select="liber:pngets('financing', $financing)"/>
                    </td>
                    <td class="total right">
                        <xsl:value-of select="liber:fm($financing)"/>
                    </td>
                </tr>
                <tr>
                    <td class="left">
                        <xsl:value-of select="liber:pngets('net-cash-flow', $netCashFlow)"/>
                    </td>
                    <td class="subtotal right">
                        <xsl:value-of select="liber:fm($netCashFlow)"/>
                    </td>
                </tr>
                <tr>
                    <td class="left">
                        <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(started))"/>
                    </td>
                    <td class="right">
                        <xsl:value-of select="liber:fm($cashPrevious)"/>
                    </td>
                </tr>
                <tr>
                    <td class="left">
                        <xsl:value-of select="concat(liber:gets('Bank'), ', ', liber:fdates(posted))"/>
                    </td>
                    <td class="grand-total right">
                        <xsl:value-of select="liber:fm($cashBalance)"/>
                    </td>
                </tr>
            </tbody>
        </table>
    </xsl:template>
</xsl:stylesheet>
