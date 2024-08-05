<?xml version="1.0" encoding="utf-8"?>
<!--
publish-page.xslt
Copyright (c) 2023-2024 Ishan Pranav. All rights reserved.
Licensed under the MIT License.
-->
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output doctype-public="-//W3C//DTD XHTML 1.0 Transitional//EN"
                doctype-system="http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"
                method="html"/>
    <xsl:template match="/">
        <xsl:variable
          name="CompanyName"
          select="//SPAN[@CLASS='BannerTextCompany']"/>
        <xsl:variable
          name="ProductName"
          select="//SPAN[@CLASS='BannerTextApplication']"/>
        <xsl:variable
          name="BootstrapperSection"
          select="//TABLE[@ID='BootstrapperSection']"/>
        <xsl:variable
          name="Version"
          select="substring-before(//BODY/TABLE/TR[2]/TD/TABLE/TR/TD/TABLE/TR[4]/TD[3], '.0')"/>
        <xsl:variable
          name="InstallerHref"
          select="//A[@ID='InstallButton']/@HREF"/>
        <html lang="en" xmlns="http://www.w3.org/1999/xhtml">
            <head>
                <meta charset="utf-8" />
                <meta name="viewport" content="width=device-width, initial-scale=1"/>
                <meta name="author" content="Ishan Pranav"/>
                <meta name="description" content="Download IshanBooks"/>
                <title>
                    <xsl:value-of select="$CompanyName"/> - <xsl:value-of select="$ProductName"/>
                </title>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-9ndCyUaIbzAi2FUVXJi0CjmCapSmO7SnpJef0486qhLnuZ2cdeRhO02iuK6FUUVM" crossorigin="anonymous"/>
                <link href="index.css" rel="stylesheet" type="text/css"/>
                <link href="favicon.ico" rel="icon" type="image/x-icon"/>
            </head>
            <body>
                <svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
                    <symbol id="arrow-right-circle" viewBox="0 0 16 16">
                        <path d="M8 0a8 8 0 1 1 0 16A8 8 0 0 1 8 0zM4.5 7.5a.5.5 0 0 0 0 1h5.793l-2.147 2.146a.5.5 0 0 0 .708.708l3-3a.5.5 0 0 0 0-.708l-3-3a.5.5 0 1 0-.708.708L10.293 7.5H4.5z"/>
                    </symbol>
                </svg>
                <div class="col-lg-8 mx-auto p-4 py-md-5">
                    <header class="d-flex align-items-center pb-3 mb-5 border-bottom">
                        <a href="/" class="d-flex align-items-center text-body-emphasis text-decoration-none">
                            <span class="fs-4">
                                <xsl:value-of select="$CompanyName"/>
                            </span>
                        </a>
                    </header>

                    <main>
                        <h1 class="text-body-emphasis">
                            <xsl:value-of select="$ProductName"/>
                        </h1>
                        <p class="fs-5 col-md-8">Double-entry accounting.</p>
                        <div class="mb-5">
                            <a href="{$InstallerHref}"
                               class="btn btn-primary btn-lg px-4 mr-1">
                                Download v<xsl:value-of select="$Version"/>
                            </a>
                            <xsl:text> </xsl:text>
                            <a href="https://github.com/ishanpranav/novatel-gnss-log-parser"
                               target="_blank"
                               class="btn btn-light btn-lg px-4">
                                View source
                            </a>
                        </div>
                        <hr class="col-3 col-md-2 mb-5"/>
                        <div class="row g-5">
                            <div class="col-md-6">
                                <h2 class="text-body-emphasis">Features</h2>
                                <ul class="list-unstyled ps-0">
                                    <li>
                                        <a class="icon-link mb-1" href="https://en.wikipedia.org/wiki/Chart_of_accounts">
                                            <svg class="bi" width="16" height="16">
                                                <use href="#arrow-right-circle"/>
                                            </svg>
                                            Chart of accounts
                                        </a>
                                    </li>
                                    <li>
                                        <a class="icon-link mb-1" href="https://en.wikipedia.org/wiki/General_journal">
                                            <svg class="bi" width="16" height="16">
                                                <use href="#arrow-right-circle"/>
                                            </svg>
                                            Make general journal entries
                                        </a>
                                    </li>
                                    <li>
                                        <a class="icon-link mb-1" href="https://en.wikipedia.org/wiki/Checkwriter">
                                            <svg class="bi" width="16" height="16">
                                                <use href="#arrow-right-circle"/>
                                            </svg>
                                            Write checks
                                        </a>
                                    </li>
                                </ul>
                            </div>
                            <div class="col-md-6">
                                <h2 class="text-body-emphasis">Prerequisities</h2>
                                <p>
                                    <xsl:value-of select="$BootstrapperSection/TR[1]/TD"/>
                                </p>
                                <ul class="list-unstyled ps-0">
                                    <xsl:for-each select="$BootstrapperSection/TR/TD/UL/LI">
                                        <li>
                                            <a class="icon-link mb-1" href="#">
                                                <svg class="bi" width="16" height="16">
                                                    <use href="#arrow-right-circle"/>
                                                </svg>
                                                <xsl:value-of select="."/>
                                            </a>
                                        </li>
                                    </xsl:for-each>
                                </ul>
                                <p>
                                    <xsl:copy-of select="$BootstrapperSection/TR[3]/TD"/>
                                </p>
                                <a href="{$InstallerHref}"
                                   class="btn btn-light btn-md px-4">Download installer</a>
                            </div>
                        </div>
                    </main>
                    <footer class="pt-5 my-5 text-body-secondary border-top">
                        Created by <xsl:value-of select="$CompanyName"/> &#183; &#169; 2023-2024
                    </footer>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>

