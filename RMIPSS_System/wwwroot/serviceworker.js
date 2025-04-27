const CACHE_NAME = "RMIPSS-CACHE-V1";
const urlsToCache = [
    "/",
    "/css/site.css",
    "/js/indexedDB.js",
    "/js/processSteps.js",
    "/js/referralForm.js",
    "/js/site.js",
    "/offline.html",
    "/Referral/CreateReferralForm",
    "/ConsentForm/Create",
    "/ConsentForm/ConsentFormEvaluationReevaluation"
];

// Install service worker and cache files
self.addEventListener("install", event => {
    event.waitUntil(
        caches.open(CACHE_NAME).then(cache => {
            return cache.addAll(urlsToCache);
        })
    );
});

// Serve cached content when offline
self.addEventListener("fetch", event => {
    const url = new URL(event.request.url);

    if (url.pathname.includes("ping.txt")) {
        // Explicitly allow it to go to network and fail
        event.respondWith(fetch(event.request));
        return;
    }

    event.respondWith(
        caches.match(event.request).then(response => {
            return response || fetch(event.request);
        }).catch(() => {
            if (event.request.mode === "navigate") {
                return caches.match("/offline.html");
            }
            return new Response('', { status: 503, statusText: "Offline and not cached" });
        })
    );
});

// Activate service worker and remove old caches
self.addEventListener("activate", event => {
    event.waitUntil(
        caches.keys().then(cacheNames => {
            return Promise.all(
                cacheNames.map(cache => {
                    if (cache !== CACHE_NAME) {
                        return caches.delete(cache);
                    }
                })
            );
        })
    );
});