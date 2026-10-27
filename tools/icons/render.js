const { chromium } = require('playwright');
const fs = require('fs');
const [src, out] = process.argv.slice(2);
const names = ['lock','lock-open','house','square-arrow-out-up-right','square-arrow-down-left','eye','pencil','scan'];
(async () => {
  const b = await chromium.launch(); const p = await b.newPage({ deviceScaleFactor: 1 });
  for (const n of names) {
    const svg = fs.readFileSync(`${src}/${n}.svg`, 'utf8').replace(/width="24"/, 'width="48"').replace(/height="24"/, 'height="48"').replace(/currentColor/g, '#282828');
    await p.setContent(`<html><body style="margin:0;background:transparent">${svg}</body></html>`);
    await p.locator('svg').screenshot({ path: `${out}/${n}.png`, omitBackground: true });
  }
  await b.close();
})();
