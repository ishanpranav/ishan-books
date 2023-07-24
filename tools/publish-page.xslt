<?xml version="1.0" encoding="utf-8"?>
<!--
publish-page.xslt
Copyright (c) 2023 Ishan Pranav. All rights reserved.
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
                <meta name="description" content="Download GNSS log parser"/>
                <title>
                    <xsl:value-of select="$CompanyName"/> - <xsl:value-of select="$ProductName"/>
                </title>
                <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha1/dist/css/bootstrap.min.css"
                      rel="stylesheet"
                      type="text/css"
                      integrity="sha384-GLhlTQ8iRABdZLl6O3oVMWSktQOp6b7In1Zl3/Jr59b6EGGoI1aFkw7cmDA6j6gD"
                      crossorigin="anonymous"/>
                <link href="index.css"
                      rel="stylesheet"
                      type="text/css"/>
            </head>
            <body>
                <div class="container col-lg-8 mx-auto p-4 py-md-5">
                    <header class="d-flex align-items-center pb-3 mb-5 border-bottom">
                        <span class="fs-4">
                            <xsl:value-of select="$CompanyName"/>
                        </span>
                    </header>
                    <main>
                        <h1>
                            <xsl:value-of select="$ProductName"/>
                        </h1>
                        <p class="fs-5 col-md-8">
                            A cross-platform library and application for parsing NovAtel GNSS
                            command output message logs.
                        </p>
                        <div class="mb-5">
                            <a href="{$InstallerHref}"
                               class="btn btn-primary btn-lg px-4">
                                Download v<xsl:value-of select="$Version"/>
                            </a>
                            <a href="https://github.com/ishanpranav/novatel-gnss-log-parser"
                               target="_blank"
                               class="btn btn-light btn-lg px-4">
                                View source
                            </a>
                        </div>
                        <hr class="col-3 col-md-2 mb-5"/>
                        <div class="row g-5">
                            <div class="col-md-6">
                                <img src="images/Capture.png"
                                     alt="A screenshot of the application."
                                     class="img-fluid"/>
                            </div>
                            <div class="col-md-6">
                                <h2>Prerequisites</h2>
                                <p>
                                    <xsl:value-of select="$BootstrapperSection/TR[1]/TD"/>
                                </p>
                                <ul class="icon-list">
                                    <xsl:for-each select="$BootstrapperSection/TR/TD/UL/LI">
                                        <li>
                                            <xsl:value-of select="."/>
                                        </li>
                                    </xsl:for-each>
                                </ul>
                                <p>
                                    <xsl:copy-of select="$BootstrapperSection/TR[3]/TD"/>
                                </p>
                                <p>
                                    <a href="{$InstallerHref}"
                                       class="btn btn-light btn-md px-4">Download installer</a>
                                </p>
                            </div>
                        </div>
                    </main>
                    <footer class="pt-5 my-5 text-muted border-top">
                        Created by <xsl:value-of select="$CompanyName"/> &#183; &#169; 2023
                    </footer>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
