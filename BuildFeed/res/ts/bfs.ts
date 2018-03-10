/// <reference path="../../node_modules/@types/google.analytics/index.d.ts" />
/// <reference path="../../node_modules/@types/jsrender/index.d.ts" />
"use strict";

// ReSharper disable once InconsistentNaming
declare var jsrender: JQueryStatic;

module BuildFeed
{
    let ajax: XMLHttpRequest;
    let timeout: number;

    export function MobileMenuToggle(this: Element, ev: Event)
    {
        ev.preventDefault();

        const list = this.nextElementSibling;
        if (list != null)
        {
            list.classList.toggle("open");
        }
    }

    export function DropdownClick(this: Element, ev: Event)
    {
        ev.preventDefault();

        const linkParent = this.parentElement;
        if (linkParent == null)
        {
            return;
        }

        const alreadyOpen = linkParent.classList.contains("open");

        CloseDropdowns(ev);

        if (!alreadyOpen)
        {
            linkParent.classList.toggle("open");

            const menuClickCapture = document.getElementById("menu-open-overlay");
            if (menuClickCapture != null)
            {
                menuClickCapture.classList.add("open");
            }
        }
    }

    export function CloseDropdowns(ev: Event)
    {
        ev.preventDefault();

        const ddParents = document.getElementsByClassName("dropdown-parent");
        for (let i = 0; i < ddParents.length; i++)
        {
            ddParents[i].classList.remove("open");
        }

        const menuClickCapture = document.getElementById("menu-open-overlay");
        if (menuClickCapture != null)
        {
            menuClickCapture.classList.remove("open");
        }
    }

    export function SwitchTheme(this: HTMLElement, ev: Event)
    {
        ev.preventDefault();

        document.cookie = `bf_theme=${this.dataset["theme"]}; expires=Fri, 31 Dec 9999 23:59:59 GMT; path=/`;
        location.reload(true);
    }

    export function SwitchLanguage(this: HTMLElement, ev: Event)
    {
        ev.preventDefault();

        document.cookie = `bf_lang=${this.dataset["lang"]}; expires=Fri, 31 Dec 9999 23:59:59 GMT; path=/`;
        location.reload(true);
    }

    export function OpenSearch(ev: Event)
    {
        ev.preventDefault();

        const modal = document.getElementById("modal-search-overlay");
        if (modal != null)
        {
            modal.classList.add("open");
        }
    }

    export function CloseSearch(ev: Event)
    {
        ev.preventDefault();

        const modal = document.getElementById("modal-search-overlay");
        if (modal != null)
        {
            modal.classList.remove("open");
        }
    }

    export function StopClick(ev: Event)
    {
        ev.preventDefault();
        ev.stopPropagation();
    }

    export function InitiateSearch()
    {
        const resultPane = document.getElementById("modal-search-result");
        if (resultPane == null)
        {
            return;
        }

        resultPane.innerHTML = "";

        if (typeof (timeout) !== "undefined")
        {
            clearTimeout(timeout);
        }

        if (typeof (ajax) !== "undefined" && ajax.readyState !== XMLHttpRequest.DONE)
        {
            ajax.abort();
        }

        timeout = setInterval(SendSearch, 200);
    }

    export function SendSearch()
    {
        if (typeof (timeout) !== "undefined")
        {
            clearTimeout(timeout);
        }

        const modalInput = document.getElementById("modal-search-input") as HTMLInputElement;

        ajax = new XMLHttpRequest();
        ajax.onreadystatechange = CompleteSearch;
        ajax.open("GET", `/api/GetSearchResult/${modalInput.value}/`, true);
        ajax.setRequestHeader("accept", "application/json");
        ajax.send(null);
    }

    export function CompleteSearch(this: XMLHttpRequest)
    {
        if (this.readyState !== XMLHttpRequest.DONE || this.status !== 200)
        {
            return;
        }

        const resultPane = document.getElementById("modal-search-result");
        const templateContent = document.getElementById("result-template");
        if (resultPane == null || templateContent == null)
        {
            return;
        }

        const template = jsrender.templates(templateContent.innerHTML);
        const content = template.render(JSON.parse(ajax.responseText));
        resultPane.innerHTML = content;

        const resultLinks = resultPane.getElementsByTagName("a");
        for (let i = 0; i < resultLinks.length; i++)
        {
            resultLinks[i].addEventListener("click",
                (mev: MouseEvent) =>
                {
                    mev.preventDefault();
                    const modalInput = document.getElementById("modal-search-input") as HTMLInputElement;
                    ga("send", "pageview", `/api/GetSearchResult/${modalInput.value}/`);
                    location.assign((mev.currentTarget as HTMLAnchorElement).href);
                });
        }
    }

    export function BuildFeedSetup()
    {
        const ddParents = document.getElementsByClassName("dropdown-parent");
        for (let i = 0; i < ddParents.length; i++)
        {
            for (let j = 0; j < ddParents[i].childNodes.length; j++)
            {
                const el = ddParents[i].childNodes[j];

                if (el.nodeName === "A")
                {
                    el.addEventListener("click", DropdownClick);
                }
            }
        }

        const menuClickCapture = document.getElementById("menu-open-overlay");
        if (menuClickCapture != null)
        {
            menuClickCapture.addEventListener("click", CloseDropdowns);
        }

        const menuTheme = document.getElementById("settings-theme-menu");
        if (menuTheme != null)
        {
            const ddThemes = menuTheme.getElementsByTagName("a");
            for (let i = 0; i < ddThemes.length; i++)
            {
                ddThemes[i].addEventListener("click", SwitchTheme);
            }
        }

        const menuLang = document.getElementById("settings-lang-menu");
        if (menuLang != null)
        {
            const ddLangs = menuLang.getElementsByTagName("a");
            for (let i = 0; i < ddLangs.length; i++)
            {
                ddLangs[i].addEventListener("click", SwitchLanguage);
            }
        }

        const btnNav = document.getElementById("page-navigation-toggle");
        if (btnNav != null)
        {
            btnNav.addEventListener("click", MobileMenuToggle);
        }

        const btnSearch = document.getElementById("page-navigation-search");
        if (btnSearch != null)
        {
            btnSearch.addEventListener("click", OpenSearch);
        }

        const modalOverlay = document.getElementById("modal-search-overlay");
        if (modalOverlay != null)
        {
            modalOverlay.addEventListener("click", CloseSearch);
        }

        const modalDialog = document.getElementById("modal-search");
        if (modalDialog != null)
        {
            modalDialog.addEventListener("click", StopClick);
        }

        const modalInput = document.getElementById("modal-search-input");
        if (modalInput != null)
        {
            modalInput.addEventListener("keyup", InitiateSearch);
        }
    }
}

window.addEventListener("load", BuildFeed.BuildFeedSetup);