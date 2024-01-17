using System.Text;
using BLL.ASAMPL.KeyWordsModel;
using BLL.ASAMPL;
using BLL.ASAMPL.AbstractModel;
using BLL.ASAMPL.DataContainerModel;
using BLL.ASAMPL.DataContainerModel.ActionFormatter;
using BLL.Integration;

namespace test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //GenerateTestAsamplCode();
            //TestTupleBlock();
            //ElementsTest();+
            //ActionsTest();
            //TestAgregateBlock();+
            //TestSetsBlock();+
            //TestRenderersBlock();+
            //TestLibraryBlock();+
            //TestHandlerBlock();+
            //TestSourceBlock();+
            //TestActionDownload();+
            //TestActionUpload();+
            //TestActionTimelineWithDownload();+
            TestActionIfThen();
            //TestRenderAction();+
            //TestSubstituteAction();+
            //TestActionCaseWithElse()?;
            //TestSequenceAction();
        }

        #region single tests

        public void GithubTest()
        {
            var git = new GitManager();
        }

        public static void TestSequenceAction()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();
            var renderersModel = new RenderersKeyWordModel();

            // Додавання сетів для рендерів
            setsModel.AddSet("RendererPath", "LINK");

            // Отримання сетів для створення елементів
            var rendererPathSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "RendererPath");

            // Додавання рендерів
            renderersModel.AddRenderer("VisualRen", "C:\\Renderer\\vslren.exe");
            renderersModel.AddRenderer("AudioRen", "C:\\Renderer\\audren.exe");
            renderersModel.AddRenderer("OlfactRen", "C:\\Renderer\\olfren.exe");

            // Створення елементів для дії RENDER
            elementsModel.AddElement("ForestScene", rendererPathSet);
            elementsModel.AddElement("VisualRen", "C:\\Renderer\\vslren.exe");
            elementsModel.AddElement("AudioRen", "C:\\Renderer\\audren.exe");
            elementsModel.AddElement("OlfactRen", "C:\\Renderer\\olfren.exe");

            // Отримання елементів для дії RENDER
            var forestSceneElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "ForestScene");
            var visualRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "VisualRen");
            var audioRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "AudioRen");
            var olfactRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "OlfactRen");

            // Створення дії RENDER
            var renderAction = new ActionDataContainer("RENDER", new List<AsamplKeyWordDataContainer>
            {
                forestSceneElement,
                visualRenElement,
                audioRenElement,
                olfactRenElement
            });

            actionsModel.AddAction(renderAction);

            // Створення SEQUENCE дії
            var sequenceAction = new ActionDataContainer("SEQUENCE", new List<AsamplKeyWordDataContainer>
            {
                renderAction,
            });

            actionsModel.AddAction(sequenceAction);

            // Форматування SEQUENCE дії
            var formatter = new SequenceActionFormatter();
            var formattedSequenceAction = formatter.FormatAction(sequenceAction);

            // Виведення сформованої дії
            Console.WriteLine(actionsModel.GetFormattedActions());
        }

        public static void TestActionCaseWithElse()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("Type", "TEXT");

            var tmp = setsModel.Sets.FirstOrDefault(s => s.DataName == "Type");
            // Створення елементів для дії CASE
            elementsModel.AddElement("tmp", tmp);

            elementsModel.AddElement("CaseVariable", "SomeValue"); // Використовуємо останній доданий сет
            elementsModel.AddElement("Option1", "Option1Value");
            elementsModel.AddElement("Option2", "Option2Value");
            elementsModel.AddElement("ElseOption", "ElseOptionValue");

            // Отримання елементів для дії CASE
            var caseVariableElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "CaseVariable");
            var option1Element = elementsModel.Elements.FirstOrDefault(e => e.DataName == "Option1");
            var option2Element = elementsModel.Elements.FirstOrDefault(e => e.DataName == "Option2");

            // Створення дії CASE
            var caseAction = new ActionDataContainer("CASE", new List<AsamplKeyWordDataContainer>
            {
                caseVariableElement,
                option1Element,
                option2Element,
            });

            actionsModel.AddAction(caseAction);

            // Форматування дії CASE
            var formatter = new CaseActionFormatter();
            var formattedCaseAction = formatter.FormatAction(caseAction);

            // Виведення сформованої дії
            Console.WriteLine(formattedCaseAction);
        }

        public static void TestSubstituteAction()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("VariableType", "TEXT");

            // Отримання сетів для створення елементів
            var variableTypeSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "VariableType");

            // Створення елементів для дії SUBSTITUTE
            elementsModel.AddElement("Type", variableTypeSet);
            elementsModel.AddElement("OriginalVariable", "1");
            elementsModel.AddElement("NewVariable", "2");
            elementsModel.AddElement("Condition", "Some logical condition");

            // Отримання елементів для дії SUBSTITUTE
            var originalVariableElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "OriginalVariable");
            var newVariableElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "NewVariable");
            var conditionElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "Condition");

            // Створення дії SUBSTITUTE
            var substituteAction = new ActionDataContainer("SUBSTITUTE", new List<AsamplKeyWordDataContainer>
            {
                originalVariableElement,
                newVariableElement,
                conditionElement
            });

            actionsModel.AddAction(substituteAction);

            // Форматування дії SUBSTITUTE

            // Виведення сформованої дії
            Console.WriteLine(actionsModel.GetFormattedActions());
        }

        public static void TestActionIfThen()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("LogicType", "LOGIC");
            setsModel.AddSet("FileType", "LINK");
            setsModel.AddSet("PathType", "LINK");

            // Отримання сетів
            var logicSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "LogicType");
            var fileTypeSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "FileType");
            var pathTypeSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "PathType");

            // Створення умови для IF
            elementsModel.AddElement("condition", logicSet);
            elementsModel.AddElement("condition", "true");

            // Створення параметрів для UPLOAD
            elementsModel.AddElement("sourceFile", fileTypeSet);
            elementsModel.AddElement("destinationPath", pathTypeSet);

            // Отримання елементів
            var conditionElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "condition");
            var sourceFileElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "sourceFile");
            var destinationPathElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "destinationPath");

            // Створення дії UPLOAD для THEN
            var uploadAction = new ActionDataContainer("UPLOAD", new List<AsamplKeyWordDataContainer>
                {
                    sourceFileElement,
                    destinationPathElement
                });

            // Створення дії IF THEN
            var ifThenAction = new ActionDataContainer("IF THEN", new List<AsamplKeyWordDataContainer>
                {
                    conditionElement,
                    uploadAction
                });

            actionsModel.AddAction(ifThenAction);

            // Виведення сформованих дій
            Console.WriteLine(actionsModel.GetFormated());
        }

        public static void TestRenderAction()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();
            var renderersModel = new RenderersKeyWordModel();

            // Додавання сетів для рендерів
            setsModel.AddSet("RendererPath", "LINK");

            // Отримання сетів для створення елементів
            var rendererPathSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "RendererPath");

            // Додавання рендерів
            renderersModel.AddRenderer("VisualRen", "C:\\Renderer\\vslren.exe");
            renderersModel.AddRenderer("AudioRen", "C:\\Renderer\\audren.exe");
            renderersModel.AddRenderer("OlfactRen", "C:\\Renderer\\olfren.exe");

            // Створення елементів для дії RENDER
            elementsModel.AddElement("ForestScene", rendererPathSet);
            elementsModel.AddElement("VisualRen", "C:\\Renderer\\vslren.exe");
            elementsModel.AddElement("AudioRen", "C:\\Renderer\\audren.exe");
            elementsModel.AddElement("OlfactRen", "C:\\Renderer\\olfren.exe");

            // Отримання елементів для дії RENDER
            var forestSceneElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "ForestScene");
            var visualRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "VisualRen");
            var audioRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "AudioRen");
            var olfactRenElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "OlfactRen");

            // Створення дії RENDER
            var renderAction = new ActionDataContainer("RENDER", new List<AsamplKeyWordDataContainer>
            {
                forestSceneElement,
                visualRenElement,
                audioRenElement,
                olfactRenElement
            });

            actionsModel.AddAction(renderAction);

            // Форматування дії RENDER
            var formatter = new RenderActionFormatter();
            var formattedRenderAction = actionsModel.GetFormattedActions();

            // Виведення сформованої дії
            Console.WriteLine(formattedRenderAction);
        }

        public static void TestActionTimelineWithDownload()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("Time", "ATIME");
            setsModel.AddSet("VisualData", "LINK");
            setsModel.AddSet("VisualStream", "LINK");

            // Створення елементів
            elementsModel.AddElement("startTime", setsModel.Sets.FirstOrDefault(s => s.DataName == "Time"));
            elementsModel.AddElement("endTime", setsModel.Sets.FirstOrDefault(s => s.DataName == "Time"));
            elementsModel.AddElement("stepTime", setsModel.Sets.FirstOrDefault(s => s.DataName == "Time"));
            elementsModel.AddElement("VisualDat", setsModel.Sets.FirstOrDefault(s => s.DataName == "VisualData"));
            elementsModel.AddElement("VisualDataStream", setsModel.Sets.FirstOrDefault(s => s.DataName == "VisualStream"));

            // Створення дії DOWNLOAD
            var downloadAction = new ActionDataContainer("DOWNLOAD", new List<AsamplKeyWordDataContainer>
            {
                elementsModel.Elements.FirstOrDefault(e => e.DataName == "VisualDat"),
                elementsModel.Elements.FirstOrDefault(e => e.DataName == "VisualDataStream")
            });

            // Створення дії TIMELINE
            var timelineAction = new ActionDataContainer("TIMELINE", new List<AsamplKeyWordDataContainer>
            {
                elementsModel.Elements.FirstOrDefault(e => e.DataName == "startTime"),
                elementsModel.Elements.FirstOrDefault(e => e.DataName == "endTime"),
                elementsModel.Elements.FirstOrDefault(e => e.DataName == "stepTime"),
                downloadAction
            });

            actionsModel.AddAction(timelineAction);

            // Виведення сформованих дій
            Console.WriteLine(actionsModel.GetFormated());
        }

        public static void TestActionUpload()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("File", "LINK");
            setsModel.AddSet("Path", "LINK");

            // Додавання елементів
            elementsModel.AddElement("sourceFile", setsModel.Sets.First(s => s.DataName == "File"));
            elementsModel.AddElement("destinationPath", setsModel.Sets.First(s => s.DataName == "Path"));

            // Створення дії UPLOAD
            var uploadActionData = new List<AsamplKeyWordDataContainer>
            {
                elementsModel.Elements.First(e => e.DataName == "sourceFile"),
                elementsModel.Elements.First(e => e.DataName == "destinationPath")
            };

            var uploadFormatter = new UploadActionFormatter();
            var uploadAction = new ActionDataContainer("UPLOAD", uploadActionData);

            actionsModel.AddAction(uploadAction);

            // Виведення сформованих дій
            Console.WriteLine(uploadFormatter.FormatAction(uploadAction));
        }

        public static void TestActionDownload()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();
            var actionsModel = new ActionsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("File", "LINK");
            setsModel.AddSet("Path", "LINK");

            // Отримання сетів для створення елементів
            var fileSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "File");
            var pathSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "Path");

            // Створення елементів для дії DOWNLOAD
            elementsModel.AddElement("sourceFile", fileSet);
            elementsModel.AddElement("destinationPath", pathSet);

            // Отримання елементів для дії DOWNLOAD
            var sourceElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "sourceFile");
            var destinationElement = elementsModel.Elements.FirstOrDefault(e => e.DataName == "destinationPath");

            // Створення дії DOWNLOAD
            var downloadAction = new ActionDataContainer("DOWNLOAD", new List<AsamplKeyWordDataContainer>
            {
                sourceElement,
                destinationElement
            });

            actionsModel.AddAction(downloadAction);

            // Виведення сформованих дій
            Console.WriteLine(actionsModel.GetFormated());
        }

        public static void TestAgregateBlock()
        {
            var setModel = new SetsKeyWordModel();
            var tupleModel = new TupleKeyWordModel();
            var aggregateModel = new AggregateKeyWordModel();

            // Додавання множин
            setModel.AddSet("Frame", "Integer");
            setModel.AddSet("Audio", "Real");
            setModel.AddSet("Scent", "Text");

            // Додавання кортежів
            tupleModel.AddTuple("VisualDat", new List<SetDataContainer> { new SetDataContainer("Frame", "Integer") });
            tupleModel.AddTuple("AudioDat", new List<SetDataContainer> { new SetDataContainer("Audio", "Real") });

            // Додавання додаткових множин до існуючого кортежу
            tupleModel.AddTuple("VisualDat", new List<SetDataContainer> { new SetDataContainer("Scent", "Text") });

            // Додавання агрегатів
            aggregateModel.AddAggregate("ForestScene", tupleModel.Tuples);
            Console.WriteLine(aggregateModel.GetFormated());
            Console.WriteLine(aggregateModel.Aggregates[0].DataInfo[0].GetDataRepresentation());

            // Додавання додаткового кортежу до існуючого агрегату
            aggregateModel.AddAggregate("ForestScene", new List<TupleDataContainer>
            {
                new TupleDataContainer("ScentDat")
            });

            // Виведення сформованих множин, кортежів та агрегатів
            Console.WriteLine(setModel.GetFormated());
            Console.WriteLine(tupleModel.GetFormated());
            Console.WriteLine(aggregateModel.GetFormated());
        }

        public static void TestTupleBlock()
        {
            // Створення моделей для ключових слів
            var setModel = new SetsKeyWordModel();
            var tupleModel = new TupleKeyWordModel();

            // Додавання множин
            setModel.AddSet("Frame", "Integer");
            setModel.AddSet("Audio", "Real");
            setModel.AddSet("Scent", "Text");

            // Додавання кортежів
            tupleModel.AddTuple("VisualDat", new List<SetDataContainer> { new SetDataContainer("Frame", "Integer") });
            tupleModel.AddTuple("AudioDat", new List<SetDataContainer> { new SetDataContainer("Audio", "Real") });
            Console.WriteLine(tupleModel.GetFormated());
            // Додавання додаткових множин до існуючого кортежу
            tupleModel.AddTuple("VisualDat", new List<SetDataContainer> { new SetDataContainer("Scent", "Text") });

            // Виведення сформованих множин та кортежів
            Console.WriteLine(setModel.GetFormated());
            Console.WriteLine(tupleModel.GetFormated());
        }

        /* private static void TestActionsBlock()
         {
             var actionsBlock = new ActionsKeyWordModel();

             // Додавання дій
             actionsBlock.AddAction("DOWNLOAD AudioDat FROM VideoFile.audio WITH MPEG2tuple");
             actionsBlock.AddAction("DOWNLOAD VisualDat2 FROM VideoFile.visual WITH MPEG2tuple");
             actionsBlock.AddAction("DOWNLOAD OlfactDat FROM OlfactFile WITH default.OlfactLib");
             actionsBlock.AddAction("TIMELINE time1 : step : time2 { DOWNLOAD VisualDat FROM VisualDataStream WITH default.VisualLib }");
             actionsBlock.AddAction("SUBSTITUTE VisualDat2 FOR VisualDat WHEN VisualDataStream IS Equivalent Null");
             actionsBlock.AddAction("UPLOAD ForestScene TO SceneFile WITH default.all");
             actionsBlock.AddAction("RENDER ForestScene WITH [VisualRen, AudioRen, OlfactRen]");

             // Виведення відформатованого списку дій
             Console.WriteLine("\nFormatted Actions:");
             Console.WriteLine(actionsBlock.GetFormattedActions());

             // Виконання блоку (для демонстрації)
             actionsBlock.Execute();
         }*/

        private static void ElementsTest()
        {
            var setsModel = new SetsKeyWordModel();
            var elementsModel = new ElementsKeyWordModel();

            // Додавання сетів
            setsModel.AddSet("Frame", "INTEGER");
            setsModel.AddSet("Audio", "REAL");
            setsModel.AddSet("Time", "ATIME");

            // Виведення сформованих сетів
            Console.WriteLine(setsModel.GetFormated());

            // Додавання елементів, які використовують сети
            var frameSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "Frame");
            var audioSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "Audio");
            var timeSet = setsModel.Sets.FirstOrDefault(s => s.DataName == "Time");

            if (frameSet != null)
            {
                elementsModel.AddElement("VisualDat", frameSet);
            }

            if (audioSet != null)
            {
                elementsModel.AddElement("AudioDat", audioSet);
            }

            if (timeSet != null)
            {
                elementsModel.AddElement("TimeDat", timeSet);
            }
            Console.WriteLine(elementsModel.GetFormated());
            // Додавання звичайних значень до елементів
            elementsModel.AddElement("time1", "00:00:01");
            Console.WriteLine($"{elementsModel.Elements.Last().ElementType}");
            elementsModel.AddElement("time2", "00:15:00");
            elementsModel.AddElement("step", "00:00:01");

            // Виведення сформованих елементів
            Console.WriteLine(elementsModel.GetFormated());
        }

        private static void TestSetsBlock()
        {
            var setsBlock = new SetsKeyWordModel();

            // Додавання множин
            bool added1 = setsBlock.AddSet("Frame", "Integer");
            bool added2 = setsBlock.AddSet("Audio", "Real");
            bool added3 = setsBlock.AddSet("Scent", "Text");

            // Виведення результатів додавання
            Console.WriteLine($"Frame added: {added1}");
            Console.WriteLine($"Audio added: {added2}");
            Console.WriteLine($"Scent added: {added3}");

            // Спроба додати існуючу множину
            bool addedDuplicate = setsBlock.AddSet("Frame", "Integer");
            Console.WriteLine($"Attempt to add duplicate Frame: {addedDuplicate}");

            // Виведення відформатованого списку множин
            Console.WriteLine("\nFormatted Sets:");
            Console.WriteLine(setsBlock.GetFormattedSets());

            // Виконання блоку (для демонстрації)
            setsBlock.Execute();
        }

        private static void TestRenderersBlock()
        {
            var renderersBlock = new RenderersKeyWordModel();

            // Додавання рендерерів
            bool added1 = renderersBlock.AddRenderer("VisualRen", @"C:\Renderer\vslren.exe");
            bool added2 = renderersBlock.AddRenderer("AudioRen", @"C:\Renderer\audren.exe");
            bool added3 = renderersBlock.AddRenderer("OlfactRen", @"C:\Renderer\olfren.exe");

            // Виведення результатів додавання
            Console.WriteLine($"VisualRen added: {added1}");
            Console.WriteLine($"AudioRen added: {added2}");
            Console.WriteLine($"OlfactRen added: {added3}");

            // Спроба додати існуючий рендерер
            bool addedDuplicate = renderersBlock.AddRenderer("VisualRen", @"C:\Renderer\vslren.exe");
            Console.WriteLine($"Attempt to add duplicate VisualRen: {addedDuplicate}");

            // Виведення відформатованого списку рендерерів
            Console.WriteLine("\nFormatted Renderers:");
            Console.WriteLine(renderersBlock.GetFormattedRenderers());

            // Виконання блоку (для демонстрації)
            renderersBlock.Execute();
        }

        private static void TestLibraryBlock()
        {
            var libraryBlock = new LibraryKeyWordModel();

            // Додавання бібліотек
            bool added1 = libraryBlock.AddLibrary("VisualLib", @"D:\MImage\Library\vsl.lib");
            bool added2 = libraryBlock.AddLibrary("AudioLab", @"D:\MImage\Library\aud.lib");
            bool added3 = libraryBlock.AddLibrary("OlfactLib", @"D:\MImage\Library\olf.lib");

            // Виведення результатів додавання
            Console.WriteLine($"VisualLib added: {added1}");
            Console.WriteLine($"AudioLab added: {added2}");
            Console.WriteLine($"OlfactLib added: {added3}");

            // Виведення відформатованого списку бібліотек
            Console.WriteLine("\nFormatted Libraries:");
            Console.WriteLine(libraryBlock.GetFormattedLibraries());

            // Виконання блоку (для демонстрації)
            libraryBlock.Execute();
        }

        /* private static void TestAggregatesAndTuples()
         {
             // Створення та ініціалізація блоку кортежів
             var tupleBlock = new TupleKeyWordModel();
             tupleBlock.AddTuple("VisualDat", "Frame");
             tupleBlock.AddTuple("AudioDat", "Audio");
             tupleBlock.AddTuple("OlfactDat", "Scent");

             // Виведення інформації про блок кортежів
             Console.WriteLine("Tuple Block:");
             Console.WriteLine(tupleBlock.GetFormattedTuples());

             // Створення та ініціалізація блоку агрегатів
             var aggregateBlock = new AggregateKeyWordModel();
             aggregateBlock.AddAggregate("ForestScene", tupleBlock.GetTuplesNamesList());

             // Виведення інформації про блок агрегатів
             Console.WriteLine("\nAggregate Block:");
             Console.WriteLine(aggregateBlock.GetFormattedAggregates());
         }*/

        private static void TestHandlerBlock()
        {
            var HandlerTest = new HandlerKeyWordModel();
            HandlerTest.AddHandler("ffmpg.txt", "C:\\New folder (10)");
            HandlerTest.AddHandler("1.txt", "C:\\New folder (10)");

            Console.WriteLine(HandlerTest.KeyWordName);
            Console.WriteLine(HandlerTest.Description);
            Console.WriteLine(HandlerTest.GetFormattedHandlers());

            var list = HandlerTest.Handlers;

            foreach (var VARIABLE in list)
            {
                Console.WriteLine(VARIABLE.DataName + VARIABLE.DataInfo);
            }
        }

        private static void TestSourceBlock()
        {
            var sourceTest = new SourceKeyWordModel();
            sourceTest.AddSource("videoIn", "E:/video.mp4");
            sourceTest.AddSource("audioIn", "/tmp/data/in.ogg");
            sourceTest.AddSource("audioOut", "/tmp/data/out.ogv");

            Console.WriteLine(sourceTest.KeyWordName);
            Console.WriteLine(sourceTest.Description);
            Console.WriteLine(sourceTest.GetFormattedSources());

            var list = sourceTest.Sources;

            foreach (var source in list)
            {
                Console.WriteLine(source.DataName + source.DataInfo);
            }
        }

        #endregion single tests
    }
}